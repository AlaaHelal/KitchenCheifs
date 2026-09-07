using System;
using UnityEngine;

public class PlayerController : MonoBehaviour, IKitchenObjectParent {

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private LayerMask interactLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;

    public event EventHandler OnObjectPickedUp;
    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs {
        public BaseCounter selectedCounter;
    }
    public static PlayerController Instance { get; private set; }

    private bool isWalking;
    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObjects kitchenObject;


    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There is more than one PlayerController instance");
        }
        Instance = this;
    }

    private void Start() {
        inputManager.OnInteractAction += InputManager_OnInteractAction;
        inputManager.OnInteractAlternateAction += InputManager_OnInteractAlternateAction;
    }

    private void InputManager_OnInteractAlternateAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void InputManager_OnInteractAction(object sender, EventArgs e) {
        if (selectedCounter != null) {
            selectedCounter.Interact(this);
        }
    }

    private void Update() {
        //Move the player 
        HandleMovement();

        //Handle interactions
        HandlInteractions();


    }

    public bool IsWalking() {
        
        return isWalking;
    }

    private void HandleMovement() {
        //Get the input vector and convert it to a 3D movement direction
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

        //Check if the player can move in the desired direction using a capsule cast to detect collisions
        float playerRadius = 0.5f;
        float playerHeight = 2f;
        float movDistance = moveSpeed * Time.deltaTime;
        Vector3 pointA = transform.position;
        Vector3 pointB = transform.position + Vector3.up * playerHeight;
        bool canMove = !Physics.CapsuleCast(pointA, pointB, playerRadius, movDir, movDistance);

        if (!canMove) {
            //Cannot move towards movDir, attempt to move along individual axes (split movdir)

            //Attempt to move only along the X axis
            Vector3 moveDirX = new Vector3(movDir.x, 0, 0).normalized;
            canMove = movDir.x !=0 && !Physics.CapsuleCast(pointA, pointB, playerRadius, moveDirX, movDistance);
            
            if (canMove)
                //Move only along X axis
                movDir = moveDirX;
            else {
                //Attempt to move only along the Z axis
                Vector3 moveDirZ = new Vector3(0, 0, movDir.z).normalized;
                canMove = movDir.z !=0 && !Physics.CapsuleCast(pointA, pointB, playerRadius, moveDirZ, movDistance);
                if (canMove)
                    //Move only along Z axis
                    movDir = moveDirZ;
                else {
                    //Cannot move along either axis, do not move
                }
            }
        }

        //Move the player if possible
        if (canMove) {
            transform.position += movDir * moveSpeed * Time.deltaTime;
        }

        // Smoothly rotate towards movement direction
        float rotationSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotationSpeed * Time.deltaTime);

        // Update the walking state
        isWalking = movDir != Vector3.zero;

        
    }

    private void HandlInteractions() {
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();
        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (movDir != Vector3.zero) {
            lastInteractDir = movDir;
        }

        float interactDistance = 2f;
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, interactLayerMask)) {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter)) {

                if (baseCounter != selectedCounter) {
                    SetSelectedCounter(baseCounter);

                }
            } else {
                SetSelectedCounter(null);
            }
        } else {
            SetSelectedCounter(null);
        }
    }


    private void SetSelectedCounter(BaseCounter selectedCounter) {

        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs {
            selectedCounter = selectedCounter
        });

    }

    public Transform GetKichenObjectFollowTransform() {
        return kitchenObjectHoldPoint;
    }

    //This Method for the kitchen object the player is holding
    public void SetKichenObject(KitchenObjects kichenObject) {
        this.kitchenObject = kichenObject;
        if (kitchenObject != null) {
            OnObjectPickedUp?.Invoke(this, EventArgs.Empty);
        }
    }

    public KitchenObjects GetKitchenObject() {
        return kitchenObject;
    }

    public void ClearKichenObject() {
        kitchenObject = null;
    }

    public bool HasKitchenObject() {
        return kitchenObject != null;
    }
}
