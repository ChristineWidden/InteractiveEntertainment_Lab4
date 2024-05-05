using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class SpecialCollider : MonoBehaviour
{
    [SerializeField] private string collisionTag;
    public UnityEvent onStartCollision;
    public UnityEvent onEndCollision;
    public UnityEvent<string> onStartCollisionSendOther;
    public UnityEvent<string> onEndCollisionSendOther;

    public bool colliding {get; private set;}

    // private Collision2D otherCollider;

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Colliding with " + other.collider.tag);
        if(other.gameObject.CompareTag(collisionTag)) {
            colliding = true;
            onStartCollision.Invoke();
        }
        onStartCollisionSendOther.Invoke(other.collider.tag);
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.CompareTag(collisionTag)) {
            colliding = false;
            onEndCollision.Invoke();
        }
        onEndCollisionSendOther.Invoke(other.collider.tag);
    }
}