using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialCollider : MonoBehaviour
{
    [SerializeField] private List<string> collisionTags;
    public UnityEvent onStartCollision;
    public UnityEvent onEndCollision;
    public UnityEvent<string> onStartCollisionSendOther;
    public UnityEvent<string> onEndCollisionSendOther;

    public bool colliding { get; private set; }

    public int numCollisions { get; private set; }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("Colliding with " + other.collider.tag);
        if (collisionTags.Contains(other.gameObject.tag))
        {
            numCollisions++;
            colliding = true;
            onStartCollision.Invoke();
        }
        onStartCollisionSendOther.Invoke(other.collider.tag);
    }

    void OnCollisionExit2D(Collision2D other)
    {
        // Debug.Log("No longer colliding with " + other.collider.tag);
        if (collisionTags.Contains(other.gameObject.tag))
        {
            numCollisions--;
            if (numCollisions == 0)
            {
                colliding = false;
                onEndCollision.Invoke();
            }

        }
        onEndCollisionSendOther.Invoke(other.collider.tag);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionTags.Contains(other.gameObject.tag))
        {
            numCollisions++;
            colliding = true;
            onStartCollision.Invoke();
        }
        onStartCollisionSendOther.Invoke(other.gameObject.tag);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (collisionTags.Contains(other.gameObject.tag))
        {
            numCollisions--;
            if (numCollisions == 0)
            {
                colliding = false;
                onEndCollision.Invoke();
            }

        }
        onEndCollisionSendOther.Invoke(other.gameObject.tag);
    }
}