using UnityEngine;

public class KitchenObjects : MonoBehaviour
{
    [SerializeField]private KitchenObjectsSO kitchenObjectSO;

    public KitchenObjectsSO GetKichenObjectSO() { 
        return kitchenObjectSO;
    }

}
