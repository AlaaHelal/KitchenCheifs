using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInputActions inputActions;

    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized() {

        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();

        Debug.Log("Input Vector: " + inputVector);
        return inputVector.normalized;

        /*
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

        return inputVector;
        */
    }
}
