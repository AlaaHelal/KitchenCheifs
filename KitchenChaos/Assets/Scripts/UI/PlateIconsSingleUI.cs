using UnityEngine;
using UnityEngine.UI;

public class PlateIconsSingleUI : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetKitchenObjectSOSprite(KitchenObjectsSO kitchenObjectSO) {
        image.sprite = kitchenObjectSO.sprite;
    }
}
