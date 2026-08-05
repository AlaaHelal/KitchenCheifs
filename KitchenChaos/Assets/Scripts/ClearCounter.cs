using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private KitchenObjectsSO kitchenObjectSO;
    public void Interact() {
        Transform kitchenObjectPrefab = Instantiate(kitchenObjectSO.prefab, counterTopPoint);
        kitchenObjectPrefab.localPosition = Vector3.zero;
        

        Debug.Log(kitchenObjectPrefab.GetComponent<KitchenObjects>().GetKichenObjectSO().objectName);
    }
}
