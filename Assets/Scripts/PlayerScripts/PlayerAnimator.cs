using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

// Next time you think of going back to Unity's animation tools
// Escaping Unity Animator HELL
// https://www.youtube.com/watch?v=nBkiSJ5z-hE


public class GoblinAnimation
{
    public static string STAND = "Stand";
    public static string WALK = "Walk";
    public static string RUN = "Run";
    public static string CROUCH = "Crouch";
    public static string AIR = "Air";
    public static string HURT = "Hurt";
}

public class PlayerAnimator : MonoBehaviour
{
    public float standToWalkThreshold;
    public float walkToRunThreshold;
    
    public bool animatingHurt;
    public bool onGround;
    public Vector2 velocity;

    private PlayerInput playerInput;
    private Animator animator;
    private string currentState;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update() {
        if (animatingHurt) {
            ChangeAnimationState(GoblinAnimation.HURT);
        }

        if (!onGround) {
            ChangeAnimationState(GoblinAnimation.AIR);
            return;
        }

        if (playerInput.actions["Crouch"].ReadValue<float>() > 0.5f) {
            ChangeAnimationState(GoblinAnimation.CROUCH);
            return;
        }

        float absVelX = Math.Abs(velocity.x);

        if (absVelX > walkToRunThreshold) {
            ChangeAnimationState(GoblinAnimation.RUN);
            return;
        }

        if (absVelX > standToWalkThreshold) {
            ChangeAnimationState(GoblinAnimation.WALK);
            return;
        }

        ChangeAnimationState(GoblinAnimation.STAND);
    }

    public void ChangeAnimationState(string newState) {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
}