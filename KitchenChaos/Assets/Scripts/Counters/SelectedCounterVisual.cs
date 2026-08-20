using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] gameObjectVisualArray;
    private void Start() {
        PlayerController.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, PlayerController.OnSelectedCounterChangedEventArgs e)
    {
        
        if(e.selectedCounter == baseCounter)
        {
            ShowVisual();
        }
        else
        {
            HideVisual();
        }
    }

    private void ShowVisual() { 
        foreach (GameObject gameObject in gameObjectVisualArray) {
            gameObject.SetActive(true);
        }
    }

    private void HideVisual() {
        foreach (GameObject gameObject in gameObjectVisualArray) {
            gameObject.SetActive(false);
        }
    }
}
