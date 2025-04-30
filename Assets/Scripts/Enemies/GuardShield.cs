using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

// Next time you think of going back to Unity's animation tools
// Escaping Unity Animator HELL
// https://www.youtube.com/watch?v=nBkiSJ5z-hE



public class GuardShield : MonoBehaviour
{
    [SerializeField] private SpriteRenderer parentRenderer;
    [SerializeField] private BoxCollider2D bcollider;

    private float baseX;

    void Start()
    {
        baseX = bcollider.offset.x;
    }

    void Update()
    {
        Vector2 pos = bcollider.offset;
        if (parentRenderer.flipX)
        {
            pos.x = baseX;
        }
        else
        {
            pos.x = -1 * baseX;

        }
        bcollider.offset = pos;
    }

}