using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private float rotationSpeed = 10f;

    

    
    private void Update()
    {
        // Must initialize inputVector to zero inside update to avoid the input from keeping going in the same direction
        Vector2 inputVector = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        inputVector = inputVector.normalized;
        Vector3 movDir = new Vector3(inputVector.x, 0, inputVector.y);

        transform.position += movDir * moveSpeed * Time.deltaTime;

        transform.forward = Vector3.Slerp(transform.forward, movDir, rotationSpeed * Time.deltaTime);
    }
}
