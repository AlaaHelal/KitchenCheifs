using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField]private KitchenObjectsSO kitchenObjectSO;

    private IKitchenObjectParent kitchenObjectParent;

    public KitchenObjectsSO GetKichenObjectSO() { 
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent) {

        if(this.kitchenObjectParent != null) {
            
            this.kitchenObjectParent.ClearKichenObject();
        }

        this.kitchenObjectParent = kitchenObjectParent;

        if (kitchenObjectParent.HasKitchenObject()){ 
            Debug.LogError("Parent already has a kitchen object!");
        }

        kitchenObjectParent.SetKichenObject(this);

        transform.parent = kitchenObjectParent.GetKichenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    }
