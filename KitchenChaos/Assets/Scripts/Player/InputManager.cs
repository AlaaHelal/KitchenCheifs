using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    PlayerInputActions inputActions;
    public event EventHandler OnInteractAction;
    public event EventHandler OnInteractAlternateAction;

    private void Awake() {
        inputActions = new PlayerInputActions();
        inputActions.Player.Enable();

        inputActions.Player.Interact.performed += Interact_performed;
        inputActions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    private void InteractAlternate_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnInteractAlternateAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
       OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMovementVectorNormalized() {

        Vector2 inputVector = inputActions.Player.Move.ReadValue<Vector2>();
        return inputVector.normalized;
    }
}
