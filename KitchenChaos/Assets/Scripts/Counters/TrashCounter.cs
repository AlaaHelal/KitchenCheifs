using UnityEngine;

public class TrashCounter : BaseCounter
{
    override public void Interact(PlayerController player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
        }
    }   
}
