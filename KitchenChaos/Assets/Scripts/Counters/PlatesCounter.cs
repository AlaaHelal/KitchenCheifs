using System;
using UnityEngine;

public class PlatesCounter : BaseCounter {

    public event EventHandler onPlateSpawned;
    public event EventHandler onPlateRemoved;

    [SerializeField] KitchenObjectsSO plateKitchenObjectSO;
    
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 4f;

    private int spawnPlatesAmount;
    private int spawnPlatesAmountMax = 4;

    private void Update() {
        spawnPlateTimer += Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax) {
            spawnPlateTimer = 0f;
            
            if(spawnPlatesAmount < spawnPlatesAmountMax) {
                spawnPlatesAmount++;

                onPlateSpawned.Invoke(this, EventArgs.Empty);

            }
        }

    }

    public override void Interact(PlayerController player) {
        if (!player.HasKitchenObject()) {
            // player not carring anything
            if (spawnPlatesAmount >= 1) {
                spawnPlatesAmount--;

                KitchenObjects.SpawnKichenObject(player, plateKitchenObjectSO);
                onPlateRemoved.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
