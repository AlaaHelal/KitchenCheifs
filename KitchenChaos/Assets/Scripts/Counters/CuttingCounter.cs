using System;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    public event EventHandler OnCut;
    public event EventHandler<OnProgressChangedEventHandler> OnProgressChanged;
    public class OnProgressChangedEventHandler : EventArgs {
        public float progressNormalized;
    }

    [SerializeField] private CutRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(PlayerController player) {
        if (!HasKitchenObject()) {
            // There is no kitchen object here

            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKichenObjectSO())) {

                // Player is carrying something AND it can be cut = Give it to the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);

                cuttingProgress = 0;
                OnProgressChanged?.Invoke(this, new OnProgressChangedEventHandler {
                    progressNormalized = 0
                });

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

        // Check if there is a kitchen object here AND if it can be cut 
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKichenObjectSO())) {

            CutRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(GetKitchenObject().GetKichenObjectSO());

            cuttingProgress++;
            OnProgressChanged?.Invoke(this, new OnProgressChangedEventHandler {
                progressNormalized = cuttingProgress / (float)cuttingRecipeSO.cuttingProgressMax
            });

            OnCut?.Invoke(this, EventArgs.Empty);

            // KitchenObjectsSO outputKichenObjectSO = GetOuputForInput(GetKitchenObject().GetKichenObjectSO());
            if (cuttingProgress == cuttingRecipeSO.cuttingProgressMax) {

                GetKitchenObject().DestroySelf();
                KitchenObjects.SpawnKichenObject(this, cuttingRecipeSO.output);
            }
            

        }
    }

    private KitchenObjectsSO GetOuputForInput(KitchenObjectsSO kitchenObjectsSO) {
        CutRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(kitchenObjectsSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        return null;

    }

    private bool HasRecipeWithInput(KitchenObjectsSO kitchenObjectsSO) {
        CutRecipeSO cuttingRecipeSO = GetCuttingRecipeWithInput(kitchenObjectsSO);
        return cuttingRecipeSO != null;
    }

    private CutRecipeSO GetCuttingRecipeWithInput(KitchenObjectsSO kitchenObjectsSO) {
        foreach (CutRecipeSO cutRecipeSO in cuttingRecipeSOArray) {
            if (cutRecipeSO.input == kitchenObjectsSO) {
                return cutRecipeSO;
            }
        }
        return null;
    }
}
