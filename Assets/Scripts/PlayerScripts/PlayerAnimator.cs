using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class PlayerAnimator : MonoBehaviour
{
    const String ANIM_STAND = "Stand";
    const String ANIM_WALK = "Walk";
    const String ANIM_RUN = "Run";
    const String ANIM_CROUCH = "Crouch";
    const String ANIM_AIR = "Air";
    const String ANIM_HURT = "Hurt";

    public bool isCrouching;
    public float animatingHurt;
    public bool onGround;

    public Vector2 velocity;

    private Animator animator;
    
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update() {
        if (animatingHurt > 0) {
            animatingHurt = animatingHurt - 1;
        }

        if (animatingHurt != 0) {
            return;
        }

        if (animatingHurt > 0) {
            ChangeAnimationState(ANIM_HURT);
        } if (isCrouching) {
            ChangeAnimationState(ANIM_CROUCH);
        }else if (onGround) {
            float absVelocityX = Mathf.Abs(velocity.x);

            if (absVelocityX < 0.001) {
                ChangeAnimationState(ANIM_STAND);
            } else if (absVelocityX < 2) {
                ChangeAnimationState(ANIM_WALK);
            } else {
                ChangeAnimationState(ANIM_RUN);
            }
        } else {
            ChangeAnimationState(ANIM_AIR);
        }
    }

    void ChangeAnimationState(string newState) {
        animator.Play(newState);
    }
}