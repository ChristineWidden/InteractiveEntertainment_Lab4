using UnityEngine;
using UnityEngine.Events;

public class SpecialCollider : MonoBehaviour
{
    [SerializeField] private string collisionTag;
    public UnityEvent onStartCollision;
    public UnityEvent onEndCollision;
    public UnityEvent<string> onStartCollisionSendOther;
    public UnityEvent<string> onEndCollisionSendOther;

    public bool colliding { get; private set; }

    private int numCollisions;

    // private Collision2D otherCollider;

    void OnCollisionEnter2D(Collision2D other)
    {
        // Debug.Log("Colliding with " + other.collider.tag);
        if (other.gameObject.CompareTag(collisionTag))
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
        if (other.gameObject.CompareTag(collisionTag))
        {
            numCollisions--;
            colliding = false;
            if (numCollisions == 0)
            {
                onEndCollision.Invoke();
            }

        }
        onEndCollisionSendOther.Invoke(other.collider.tag);
    }
}