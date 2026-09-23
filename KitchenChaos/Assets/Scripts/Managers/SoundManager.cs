using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance;

    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private float volume = 1f;


    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 1f);                              
    }

    private void Start() {
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_OnDeliveryFailed;
        DeliveryManager.Instance.OnDeliverySuccessed += DeliveryManager_OnDeliverySuccessed;
        CuttingCounter.OnAnyCut += CuttingCounter_OnAnyCut;
        PlayerController.Instance.OnObjectPickedUp += Player_OnObjectPickedUp;
        BaseCounter.OnObjectDropped += BaseCounter_OnObjectDropped;
        TrashCounter.OnAnyObjectTrashed += TrashCounter_OnAnyObjectTrashed;
       // StoveCounter.Instance.OnStateChanged += StoveCounter_OnStateChanged;
    }

    /*
    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
       
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound) {
            PlaySound(//needsArray here)
        }
        
    }*/

    private void TrashCounter_OnAnyObjectTrashed(object sender, System.EventArgs e) {
        TrashCounter trashCounter = sender as TrashCounter;
        PlaySound(audioClipRefsSO.objectTrashed, trashCounter.transform.position);
    }

    private void BaseCounter_OnObjectDropped(object sender, System.EventArgs e) {
        BaseCounter baseCounter = sender as BaseCounter;
        PlaySound(audioClipRefsSO.objectDropped, baseCounter.transform.position);
    }

    private void Player_OnObjectPickedUp(object sender, System.EventArgs e) {
        PlaySound(audioClipRefsSO.objectPickedUp, PlayerController.Instance.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e) {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);

    }

    private void DeliveryManager_OnDeliverySuccessed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccessed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_OnDeliveryFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFailed, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] clipsArray, Vector3 position, float volumeMultiplier = 1.0f) {
        AudioSource.PlayClipAtPoint(clipsArray[Random.Range(0,clipsArray.Length )], position,volumeMultiplier * volume);

    }

    public void PlayFootStepsSound() {
        AudioClip footStepsClip = audioClipRefsSO.footSteps[Random.Range(0, audioClipRefsSO.footSteps.Length)];
        float volume = this.volume;
        AudioSource.PlayClipAtPoint(footStepsClip, PlayerController.Instance.transform.position, volume);
    }


    public void ChangeVolume() {
        volume += 0.1f;
        if(volume > 1f) {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}
