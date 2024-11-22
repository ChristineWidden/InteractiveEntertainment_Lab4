using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;


public class MyAnimator: MonoBehaviour
{

    private string currentState;
    private Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState) {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;

    }

    public string getCurrentAnimationState()
    {
        return currentState;
    }

}