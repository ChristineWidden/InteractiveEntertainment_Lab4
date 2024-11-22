using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
    public string currentState;

    private MyAnimator myAnimator;

    [SerializeField] private Sprite[] standardSprites;
    [SerializeField] private Sprite[] poweredUpSprites;


    void Start()
    {
        myAnimator = GetComponent<MyAnimator>();
        playerInput = GetComponent<PlayerInput>();
    }

    void Update() {
        if (OptionsManager.Instance.IsPaused) return;
        
        if (animatingHurt) {
            myAnimator.ChangeAnimationState(GoblinAnimation.HURT);
            return;
        }

        if (!onGround) {
            myAnimator.ChangeAnimationState(GoblinAnimation.AIR);
            return;
        }

        if (playerInput.actions["Crouch"].ReadValue<float>() > 0.5f) {
            myAnimator.ChangeAnimationState(GoblinAnimation.CROUCH);
            return;
        }

        float absVelX = Math.Abs(velocity.x);

        if (absVelX > walkToRunThreshold) {
            myAnimator.ChangeAnimationState(GoblinAnimation.RUN);
            return;
        }

        if (absVelX > standToWalkThreshold) {
            myAnimator.ChangeAnimationState(GoblinAnimation.WALK);
            return;
        }

        myAnimator.ChangeAnimationState(GoblinAnimation.STAND);
    }


}