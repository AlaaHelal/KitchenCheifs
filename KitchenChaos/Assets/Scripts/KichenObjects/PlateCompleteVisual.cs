using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject { 
        public GameObject ingredientKitchenObject;
        public KitchenObjectsSO ingredientKitchenObjectSO;
    }

    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> kitchenObjectSOGameObjectList;


    private void Start() {

        plateKitchenObject.onIngredientsAdded += PlateKitchenObject_onIngredientsAdded;
        foreach (KitchenObjectSO_GameObject kitchenObject in kitchenObjectSOGameObjectList) {
            kitchenObject.ingredientKitchenObject.SetActive(false);
        }
    }

    private void PlateKitchenObject_onIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e) {
        foreach (KitchenObjectSO_GameObject kitchenObject in kitchenObjectSOGameObjectList) {
            if(e.kitchenObjectSO == kitchenObject.ingredientKitchenObjectSO) {
                kitchenObject.ingredientKitchenObject.SetActive(true);
            }
        }
    }
}
