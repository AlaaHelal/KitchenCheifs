using System;
using UnityEngine;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent {
    public static event EventHandler OnObjectDropped;

    [SerializeField] private Transform counterTopPoint;
    

    private KitchenObjects kitchenObject;
    public virtual void Interact(PlayerController player) {
    }

    public virtual void InteractAlternate(PlayerController player) {
    }

    public Transform GetKichenObjectFollowTransform() {
        return counterTopPoint;
    }

    public void SetKichenObject(KitchenObjects kichenObject) {
        kitchenObject = kichenObject;

        if (kitchenObject != null) {
            OnObjectDropped?.Invoke(this, EventArgs.Empty);
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
