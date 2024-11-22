using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

// Next time you think of going back to Unity's animation tools
// Escaping Unity Animator HELL
// https://www.youtube.com/watch?v=nBkiSJ5z-hE



public class SpriteOutliner : IOptionObserver
{
    [SerializeField] private SpriteRenderer parentRenderer;
    [SerializeField] private MyAnimator parentAnimator;
    private SpriteRenderer spriteRenderer;
    private MyAnimator myAnimator;

    public override void OnOptionChanged()
    {
        bool active = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.SPRITE_OUTLINES_ON);

        spriteRenderer.enabled = active;
    }

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<MyAnimator>();
        OnOptionChanged();
    }

    void Update() {
        myAnimator.ChangeAnimationState(parentAnimator.getCurrentAnimationState());
        spriteRenderer.flipX = parentRenderer.flipX;
    }

}