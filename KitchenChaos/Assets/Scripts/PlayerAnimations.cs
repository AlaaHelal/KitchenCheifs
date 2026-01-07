using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    
    private const string IS_WALKING = "IsWalking";

    private Animator animator;
    [SerializeField] PlayerController player;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
    }
}
