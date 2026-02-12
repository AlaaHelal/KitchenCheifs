using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private InputManager inputManager;
    private float rotationSpeed = 10f;
    private bool isWalking;
    

    
    private void Update()
    {
        Vector2 inputVector = inputManager.GetMovementVectorNormalized();

        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

        float playerRadius = 0.5f;
        float playerHeight = 2f;
        float mavDistance = moveSpeed * Time.deltaTime;
        Vector3 pointA = transform.position;
        Vector3 pointB = transform.position + Vector3.up * playerHeight;
        bool canMove = !Physics.CapsuleCast(pointA, pointB ,playerRadius, movDir, mavDistance);

        if (!canMove)
        {
            //Cannot move towards movDir, attempt to move along individual axes (split movdir)

            //Attempt to move only along the X axis
            Vector3 moveDirX = new Vector3(movDir.x, 0, 0);
            canMove = !Physics.CapsuleCast(pointA, pointB, playerRadius, moveDirX, mavDistance);
            if(canMove)
                //Move only along X axis
               movDir = moveDirX;
            else
            {
                //Attempt to move only along the Z axis
                Vector3 moveDirZ = new Vector3(0, 0, movDir.z);
                canMove = !Physics.CapsuleCast(pointA, pointB, playerRadius, moveDirZ, mavDistance);
                if (canMove)
                    //Move only along Z axis
                    movDir = moveDirZ;
                else {
                    //Cannot move along either axis, do not move
                }
            }      
        }

        if (canMove){
            transform.position += movDir * moveSpeed * Time.deltaTime;
        }
        // Smoothly rotate towards movement direction
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotationSpeed * Time.deltaTime);

        
        isWalking = movDir!= Vector3.zero;
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
