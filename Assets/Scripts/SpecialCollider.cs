using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpecialCollider : MonoBehaviour
{
    [SerializeField] private List<string> collisionTags;
    public UnityEvent<Collider2D> onStartCollision;
    public UnityEvent<Collider2D> onEndCollision;
    public UnityEvent<Collider2D> onStartCollisionSendOther;
    public UnityEvent<Collider2D> onEndCollisionSendOther;

    public bool colliding { get; private set; }

    public int numCollisions { get; private set; }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("Colliding with " + other.collider.tag);
        if (collisionTags.Contains(other.gameObject.tag))
        {
            numCollisions++;
            colliding = true;
            onStartCollision.Invoke(other.collider);
        }
        onStartCollisionSendOther.Invoke(other.collider);
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
                onEndCollision.Invoke(other.collider);
            }

        }
        onEndCollisionSendOther.Invoke(other.collider);
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (collisionTags.Contains(other.gameObject.tag))
        {
            numCollisions++;
            colliding = true;
            onStartCollision.Invoke(other);
        }
        onStartCollisionSendOther.Invoke(other);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (collisionTags.Contains(other.gameObject.tag))
        {
            numCollisions--;
            if (numCollisions == 0)
            {
                colliding = false;
                onEndCollision.Invoke(other);
            }

        }
        onEndCollisionSendOther.Invoke(other);
    }
}