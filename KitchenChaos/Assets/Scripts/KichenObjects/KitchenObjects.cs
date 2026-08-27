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

        //if (kitchenObjectParent.HasKitchenObject()){ 
        //    Debug.LogError("Parent already has a kitchen object!");
        //}

        kitchenObjectParent.SetKichenObject(this);

        transform.parent = kitchenObjectParent.GetKichenObjectFollowTransform();
        transform.localPosition = Vector3.zero;
    }

    public IKitchenObjectParent GetKitchenObjectParent() {
        return kitchenObjectParent;
    }

    public void DestroySelf() {
        kitchenObjectParent.ClearKichenObject();
        Destroy(gameObject);
    }

    public bool TryGetPlate(out PlateKitchenObject plateKitchenObject) {
        if (this is PlateKitchenObject) {
            plateKitchenObject = this as PlateKitchenObject;
            return true;
        }
        else {
            plateKitchenObject = null;
            return false;
        }
    }

    public static KitchenObjects SpawnKichenObject(IKitchenObjectParent kitchenObjectParent, KitchenObjectsSO kitchenObjectsSO) {
        //Transform kitchenObjectPrefab = Instantiate(kitchenObjectsSO.prefab);

        KitchenObjects kitchenObject = Instantiate(kitchenObjectsSO.prefab).GetComponent<KitchenObjects>();
        kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
        return kitchenObject;
    }

    }
