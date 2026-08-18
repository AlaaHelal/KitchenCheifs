using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private KitchenObjectsSO slicedObjectPrefab;

    public override void Interact(PlayerController player) {
        if (!HasKitchenObject()) {
            // There is no kitchen object here

            if (player.HasKitchenObject()) {
                // Player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);

            } else {
                // Player is not carrying anything = Do nothing
            }

        } else {
            //There is a kichen object here
            if (!player.HasKitchenObject()) {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            } else {
                // Player is carrying something = Do nothing  
            }
        }
    }

    public override void InteractAlternate(PlayerController player) {
        if (HasKitchenObject()) {
            // There is a kitchen object here? Destroy it first and then spawn the sliced one
            GetKitchenObject().DestroySelf();
            KitchenObjects.SpawnKichenObject(this, slicedObjectPrefab);

        }
    }
}
