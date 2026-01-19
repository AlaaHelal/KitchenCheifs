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

        transform.position += movDir * moveSpeed * Time.deltaTime;

        isWalking = movDir!= Vector3.zero;

        // Smoothly rotate towards movement direction
        transform.forward = Vector3.Slerp(transform.forward, movDir, rotationSpeed * Time.deltaTime);
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
