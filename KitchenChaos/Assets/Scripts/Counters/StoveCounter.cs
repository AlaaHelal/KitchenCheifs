using System;
using UnityEngine;
using static CuttingCounter;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventHandler> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> onStateChanged;
    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private float progressNormalized;
    private float fryingTimer;
    private float burningTimer;
    private FryingRecipeSO fryingRecipeSO;
    private BurningRecipeSO burningRecipeSO;

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }
    private State state;

    private void Start() {
        state = State.Idle;
    }
    private void Update() {
        //if (HasKitchenObject()) {
            switch (state) {

                case State.Idle:
                    progressNormalized = 0;
                    break;
                case State.Frying:
                    progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax;

                    fryingTimer += Time.deltaTime;
                    if (fryingTimer > fryingRecipeSO.fryingTimerMax) {

                        GetKitchenObject().DestroySelf();
                        KitchenObjects.SpawnKichenObject(this, fryingRecipeSO.output);

                        burningRecipeSO = GetBurningRecipeWithInput(fryingRecipeSO.output);
                        burningTimer = 0;
                        state = State.Fried;
                    }
                    break;
                case State.Fried:
                    progressNormalized = burningTimer / burningRecipeSO.burningTimerMax;

                    burningTimer += Time.deltaTime;
                    if (burningTimer > burningRecipeSO.burningTimerMax) {

                        GetKitchenObject().DestroySelf();
                        KitchenObjects.SpawnKichenObject(this, burningRecipeSO.output);

                        state = State.Burned;

                    }
                    break;
                case State.Burned:
                    progressNormalized = 1;
                    break;
            }
                onStateChanged?.Invoke(this, new OnStateChangedEventArgs {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventHandler {
                    progressNormalized = progressNormalized
                });
        //} 
    }
    
    
    public override void Interact(PlayerController player) {
        if (!HasKitchenObject()) {
            // There is no kitchen object here

            if (player.HasKitchenObject() && HasRecipeWithInput(player.GetKitchenObject().GetKichenObjectSO())) {

                //Must initialize the fryingRecipe here with KO player is carrying before giving it to the counter
                fryingRecipeSO = GetFryingRecipeWithInput(player.GetKitchenObject().GetKichenObjectSO());

                // Player is carrying something AND it can be fried = Give it to the counter
                player.GetKitchenObject().SetKitchenObjectParent(this);
                fryingTimer = 0;
                state = State.Frying;



            } else {
                // Player is not carrying anything = Do nothing
            }

        } else {
            //There is a kichen object here
            if (!player.HasKitchenObject()) {
                // Player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
                //state = State.Idle;

            } else {
                // Player is carrying something plate
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject)) {

                    //give the KO to the plate then destroy it from the counter
                    if (plateKitchenObject.TryAddIngredientsToPlate(GetKitchenObject().GetKichenObjectSO())) {

                        GetKitchenObject().DestroySelf();
                        //state = State.Idle;
                    }
                }
                
            }
            state = State.Idle;

        }
    }
    private KitchenObjectsSO GetOuputForInput(KitchenObjectsSO kitchenObjectsSO) {
        FryingRecipeSO cuttingRecipeSO = GetFryingRecipeWithInput(kitchenObjectsSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        return null;

    }

    private bool HasRecipeWithInput(KitchenObjectsSO kitchenObjectsSO) {
        FryingRecipeSO FryingRecipeSO = GetFryingRecipeWithInput(kitchenObjectsSO);
        return FryingRecipeSO != null;
    }

    private FryingRecipeSO GetFryingRecipeWithInput(KitchenObjectsSO kitchenObjectsSO) {
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray) {
            if (fryingRecipeSO.input == kitchenObjectsSO) {
                return fryingRecipeSO;
            }
        }
        return null;
    }

    private BurningRecipeSO GetBurningRecipeWithInput(KitchenObjectsSO kitchenObjectsSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == kitchenObjectsSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }
}