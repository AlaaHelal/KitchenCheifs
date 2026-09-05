using UnityEngine;
using System.Collections.Generic;

public class DeliveryManager : MonoBehaviour
{
    public static DeliveryManager Instance { get; private set; }

    [SerializeField] private _RecipeListSO recipesSOList;

    private List<RecipeSO> waitingRecipesSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

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
        RecipeSO waitingRecipeSO = recipesSOList.recipesSOList[Random.Range(0, recipesSOList.recipesSOList.Count)];
        waitingRecipesSOList.Add(waitingRecipeSO);
        Debug.Log("Recipe spawned: " + waitingRecipeSO.name);
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
                            Debug.Log("Kitchen Object: " + kitchenObjectRecipeSO.objectName + 
                                " is in the recipe and on the plate");
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
                    Debug.Log("Player delivered the correct recipe: " + recipeSO.name);
                    waitingRecipesSOList.RemoveAt(i);
                    return;
                }
            }
        }
        //No matching recipe was found
        //Player did not deliver the correct recipe
        Debug.Log("Player did not deliver the correct recipe");
    }
}
