using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject particlesGameObject;
    [SerializeField] private GameObject stoveOnGameObject;


    private void Start() { 
        stoveCounter.onStateChanged += StoveCounter_onStateChanged;
    }

    private void StoveCounter_onStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        bool showVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
            particlesGameObject.SetActive(showVisual);
            stoveOnGameObject.SetActive(showVisual);
        
    }
}
