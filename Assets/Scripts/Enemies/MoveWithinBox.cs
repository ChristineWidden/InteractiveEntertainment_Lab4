using UnityEngine;

public class MoveWithinBox : MonoBehaviour
{

    [SerializeField] private Collider2D boundaryCollider;
    private GuardMovement2 guardMovement;

    void Start() {
        guardMovement = GetComponent<GuardMovement2>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D thisCollider = other.GetComponent<EdgeCollider2D>();
        if (thisCollider == boundaryCollider) {
            guardMovement.moveDirection = -1 * guardMovement.moveDirection;
        }
    }

}
