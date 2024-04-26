using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveWithinBox : MonoBehaviour
{

    // private bool frozen = false;

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
