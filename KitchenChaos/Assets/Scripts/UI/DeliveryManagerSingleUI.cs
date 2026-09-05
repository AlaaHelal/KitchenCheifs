using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private Transform iconContainer;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }


    public void SetRecipeSO(RecipeSO recipeSO) {
        recipeNameText.text = recipeSO.nameOfRecipe;
        UpdateIconsVisual(recipeSO);



    }

    private void UpdateIconsVisual(RecipeSO recipeSO) {
        foreach (Transform child in iconContainer) {

            if (child == iconTemplate) continue;

            Destroy(child.gameObject);
        }

        foreach (KitchenObjectsSO kitchenObjectSO in recipeSO.recipeKitchenObjectsSOList) {
            Transform iconImage = Instantiate(iconTemplate, iconContainer);
            iconImage.gameObject.SetActive(true);
            iconImage.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
        }
        
    }


}
