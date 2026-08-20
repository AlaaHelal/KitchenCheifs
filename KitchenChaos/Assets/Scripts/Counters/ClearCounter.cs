using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter {

    

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
}
