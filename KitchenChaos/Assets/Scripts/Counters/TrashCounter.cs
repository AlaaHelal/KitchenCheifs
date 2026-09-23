using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnAnyObjectTrashed;
    new public static void ResetStaticData() {
        OnAnyObjectTrashed = null;
    }
    override public void Interact(PlayerController player) {
        if (player.HasKitchenObject()) {
            player.GetKitchenObject().DestroySelf();
            OnAnyObjectTrashed?.Invoke(this, EventArgs.Empty);
        }
    }   
}
