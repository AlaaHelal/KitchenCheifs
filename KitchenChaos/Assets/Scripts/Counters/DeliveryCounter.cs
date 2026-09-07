using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public static DeliveryCounter Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
    public override void Interact(PlayerController player) {
        if (player.HasKitchenObject()) {
            if(player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

                //player has plate only
                DeliveryManager.Instance.DeliverRecipe(plateKitchenObject);
                player.GetKitchenObject().DestroySelf();  
            }
        }
    }
}
