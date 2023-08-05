using NarvalDev.Runner;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    private const string IS_WALKING = "IsWalking";
    private const string IS_JUMPING = "IsJumping";
    private const string IS_FALLING = "IsFalling";
    private const string IS_DASHING = "IsDashing";
    [SerializeField] private Player player;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(IS_WALKING, player.IsWalking());
        animator.SetBool(IS_JUMPING, player.IsJumping());
        animator.SetBool(IS_FALLING, player.IsJumpFalling());
        animator.SetBool(IS_DASHING, player.IsDashing());

    }
}
