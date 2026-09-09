using UnityEngine;
using UnityEngine.UI;

public class GamePayingClockUI : MonoBehaviour {
    [SerializeField] Image timerImage;

    private void Update() {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimer();
    }
}
