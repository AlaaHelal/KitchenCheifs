using UnityEngine;

public class PlayerSoundPlay : MonoBehaviour
{
    private float footStepsTimer;
    private float footStepsTimerMax = 0.1f;


    private void Update() {

        footStepsTimer -= Time.deltaTime;
        if (footStepsTimer < 0) {
            footStepsTimer = footStepsTimerMax;

            if (PlayerController.Instance.IsWalking()) {
                SoundManager.Instance.PlayFootStepsSound();
            }
        }
    }
}
