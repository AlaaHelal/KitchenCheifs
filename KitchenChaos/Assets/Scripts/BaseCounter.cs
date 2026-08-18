using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    [SerializeField] private Transform counterTopPoint;
    

    private KitchenObjects kitchenObject;
    public virtual void Interact(PlayerController player) {
        Debug.LogError("BaseCounter Interact");
    }

    public virtual void InteractAlternate(PlayerController player) {
        Debug.LogError("BaseCounter InteractAlternate");
    }

    public Transform GetKichenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKichenObject(KitchenObjects kichenObject) {
        this.kitchenObject = kichenObject;
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
