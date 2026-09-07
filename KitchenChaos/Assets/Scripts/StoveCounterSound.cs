using UnityEngine;

public class StoveCounterSound : MonoBehaviour {
    [SerializeField] StoveCounter stoveCounter;
    private AudioSource stoveAudioSource;

    private void Awake() {
        stoveAudioSource = GetComponent<AudioSource>();
    }
    private void Start() {

        stoveCounter.OnStateChanged += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound) {
            //if (!stoveAudioSource.isPlaying) {
                stoveAudioSource.Play();
            //}
        } else {
            stoveAudioSource.Pause();

        }
    }
}
