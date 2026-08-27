using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObjects {

    public event EventHandler<OnIngredientsAddedEventArgs> onIngredientsAdded;
    public class OnIngredientsAddedEventArgs : EventArgs {
        public KitchenObjectsSO kitchenObjectSO;
    }

    [SerializeField] private List<KitchenObjectsSO> validKitchenObjectsSO;

    private List<KitchenObjectsSO> plateKitchenObjectsSOList;

    private void Awake() {
        plateKitchenObjectsSOList = new List<KitchenObjectsSO>();
    }

    public bool TryAddIngredientsToPlate(KitchenObjectsSO kitchenObjectSO) {
        if(!validKitchenObjectsSO.Contains(kitchenObjectSO))
            return false;

        if (!plateKitchenObjectsSOList.Contains(kitchenObjectSO)) {
            plateKitchenObjectsSOList.Add(kitchenObjectSO);
            onIngredientsAdded.Invoke(this,new OnIngredientsAddedEventArgs {
                kitchenObjectSO = kitchenObjectSO
            });
            return true;
        } 
        else { 
            return false; }
    }
}
