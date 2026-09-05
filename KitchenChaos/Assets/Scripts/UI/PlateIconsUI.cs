using UnityEngine;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    private void Start() {
        plateKitchenObject.onIngredientsAdded += PlateKitchenObject_onIngredientsAdded;
    }

    private void PlateKitchenObject_onIngredientsAdded(object sender, PlateKitchenObject.OnIngredientsAddedEventArgs e) {
        UpdateIconsVisual();

    }

    private void UpdateIconsVisual() {
        foreach (Transform child in transform ) {
            
            if (child == iconTemplate) continue;
           
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectsSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectsSOList()) {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSOSprite(kitchenObjectSO);
            iconTransform.gameObject.SetActive(true);

        }
    }
}
