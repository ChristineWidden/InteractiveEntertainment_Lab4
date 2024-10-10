using UnityEngine;

public class MoveWithinBox : MonoBehaviour
{

    [SerializeField] private Collider2D boundaryCollider;
    private GuardMovement2 guardMovement;

    private float leftBoundary;
    private float rightBoundary;

    void Start() {
        guardMovement = GetComponent<GuardMovement2>();
        Bounds bounds = boundaryCollider.bounds;
        leftBoundary = bounds.min.x;
        rightBoundary = bounds.max.x;
    }

    void Update()
    {
        if (gameObject.transform.position.x < leftBoundary) {
            guardMovement.moveDirection = 1;
        } else if (gameObject.transform.position.x > rightBoundary) {
            guardMovement.moveDirection = -1;
        }

    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     Collider2D thisCollider = other.GetComponent<EdgeCollider2D>();
    //     if (thisCollider == boundaryCollider) {
    //         guardMovement.moveDirection = -1 * guardMovement.moveDirection;
    //     }
    // }

}
