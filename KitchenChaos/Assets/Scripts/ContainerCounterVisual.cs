using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;

    private const string OPEN_CLOSE_ANIM = "OpenClose";
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() { 
        containerCounter.OnPlayerGrabbedObject += ContainerCounter_OnPlayerGrabbedObject;
    }

    private void ContainerCounter_OnPlayerGrabbedObject(object containerCounter, EventArgs e) {
        animator.SetTrigger(OPEN_CLOSE_ANIM);
    }
}
