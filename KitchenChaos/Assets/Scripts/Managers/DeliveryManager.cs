using UnityEngine;
using System.Collections.Generic;
using System;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnDeliverySuccessed;
    public event EventHandler OnDeliveryFailed;
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private _RecipeListSO recipesSOList;

    private List<RecipeSO> waitingRecipesSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;
    private int successfulRecipesAmount;

    private void Awake() {
        waitingRecipesSOList = new List<RecipeSO>();

        if (Instance != null) {
            Debug.LogError("There is more than one DeliveryManager instance");
        }
        Instance = this;
    }

    private void Start() {
        spawnRecipeTimer = spawnRecipeTimerMax;
    }

    private void Update() {

        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipesSOList.Count < waitingRecipesMax) {
                SpawnRecipe();
            }
        }
    }


    private void SpawnRecipe() {
        RecipeSO waitingRecipeSO = recipesSOList.recipesSOList[UnityEngine.Random.Range(0, recipesSOList.recipesSOList.Count)];
        waitingRecipesSOList.Add(waitingRecipeSO);
        OnRecipeSpawned.Invoke(this, System.EventArgs.Empty);
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {

        for (int i = 0; i < waitingRecipesSOList.Count; i++) { 

            RecipeSO recipeSO = waitingRecipesSOList[i];

            //Chech if the number of ingredients on the plate is the same as the number of ingredients in the recipe
            if (recipeSO.recipeKitchenObjectsSOList.Count == plateKitchenObject.GetKitchenObjectsSOList().Count) {

                bool plateContentsMatchRecipe = true;

                //Cycling through the ingredients in the recipe
                foreach (KitchenObjectsSO kitchenObjectRecipeSO in recipeSO.recipeKitchenObjectsSOList) {
                    bool ingredientFound = false;
                    //Cycling through the ingredients in the plate
                    foreach (KitchenObjectsSO kitchenObjectPlateSO in plateKitchenObject.GetKitchenObjectsSOList()) {
                        //Check if the ingredient in the recipe is the same as the ingredient in the plate
                        if (kitchenObjectRecipeSO == kitchenObjectPlateSO) {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if (!ingredientFound) {
                        //If the ingredient in the recipe is not found on the plate, then the plate does not match the recipe
                        plateContentsMatchRecipe = false;
                    }
                }

                if (plateContentsMatchRecipe) {
                    //Player delivered the correct recipe
                    successfulRecipesAmount++;
                    waitingRecipesSOList.RemoveAt(i);
                    OnRecipeCompleted.Invoke(this, System.EventArgs.Empty);
                    OnDeliverySuccessed?.Invoke(this, System.EventArgs.Empty);
                    return;
                }
            }
        }
        //No matching recipe was found
        //Player did not deliver the correct recipe
        OnDeliveryFailed?.Invoke(this, System.EventArgs.Empty);
    }

    public List<RecipeSO> GetWaitingRecipesSOList() {
        return waitingRecipesSOList;
    }

    public int GetSuccessfulRecipesAmount() {
        return successfulRecipesAmount;
    }
}
