using System;
using UnityEngine;

public class ContainerCounter : BaseCounter {
    
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;
    public override void Interact(PlayerController player) {
        if (!player.HasKitchenObject()) {

            //player is not carrying anything, so give them the kitchen object
            KitchenObjects.SpawnKichenObject(player, kitchenObjectSO);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty); 
        }
    }
    
}
