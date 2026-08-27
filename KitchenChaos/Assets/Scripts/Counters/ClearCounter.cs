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

                // Player is carrying plate
                if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

                    //give the KO to the plate then destroy it from the counter
                    if (plateKitchenObject.TryAddIngredientsToPlate(GetKitchenObject().GetKichenObjectSO())) {

                        GetKitchenObject().DestroySelf();
                    }
                } else {
                    //player is holding sth else not plate, give it to the plate
                    if (GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if (plateKitchenObject.TryAddIngredientsToPlate(player.GetKitchenObject().GetKichenObjectSO())) {

                            player.GetKitchenObject().DestroySelf();
                        }
                    }   
                }
                
            }
        }
    }
}
