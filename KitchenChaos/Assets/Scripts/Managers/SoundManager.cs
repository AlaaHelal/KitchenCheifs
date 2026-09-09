using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        DeliveryManager.Instance.OnDeliveryFailed += DeliveryManager_onDeliveryFailed;
        DeliveryManager.Instance.OnDeliverySuccessed += DeliveryManager_onDeliverySuccessed;
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

    private void DeliveryManager_onDeliverySuccessed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccessed, deliveryCounter.transform.position);
    }

    private void DeliveryManager_onDeliveryFailed(object sender, System.EventArgs e) {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFailed, deliveryCounter.transform.position);
    }

    private void PlaySound(AudioClip[] clipsArray, Vector3 position, float volume = 1.0f) {
        AudioSource.PlayClipAtPoint(clipsArray[Random.Range(0,clipsArray.Length )], position,volume);

    }

    public void PlayFootStepsSound() {
        AudioClip footStepsClip = audioClipRefsSO.footSteps[Random.Range(0, audioClipRefsSO.footSteps.Length)];
        float volume = 1.0f;
        AudioSource.PlayClipAtPoint(footStepsClip, PlayerController.Instance.transform.position, volume);
    }
}
