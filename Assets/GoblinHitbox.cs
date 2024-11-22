using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GoblinHitbox : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;

    private float initialYPos;
    private float initialHeight;
    [SerializeField] private float crouchHitboxHeight;
    private BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        initialYPos = boxCollider2D.offset.y;
        initialHeight = boxCollider2D.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInput.actions["Crouch"].ReadValue<float>() > 0.5f) {
            // if crouching
            boxCollider2D.size = new Vector2(boxCollider2D.size.x, crouchHitboxHeight);
            float newYPos = initialYPos - initialHeight / 2 + crouchHitboxHeight/2;
            boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, newYPos);

            return;
        }
        boxCollider2D.size = new Vector2(boxCollider2D.size.x, initialHeight);
        boxCollider2D.offset = new Vector2(boxCollider2D.offset.x, initialYPos);

    }
}
