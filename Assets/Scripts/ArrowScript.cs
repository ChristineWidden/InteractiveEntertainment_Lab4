using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{

    private Vector2 moveDirection;
    public float moveSpeed;

    private Animator animator;

    private void OnEnable() {
        Invoke("Destroy", 3f);
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();

        DoAnimationStuff();
    }

    void DoAnimationStuff() {
        float r = 8 * (-1f * Mathf.Atan2(moveDirection.x, moveDirection.y) + 1f * Mathf.PI) / (2f * Mathf.PI);
        Debug.Log("ARCTAN RESULT IS " + Mathf.Atan2(moveDirection.x, moveDirection.y).ToString());
        string animation =  Mathf.Round(r).ToString();
        Debug.Log("ANIMATION IS "+ animation);
        ChangeAnimationState(animation);
    }

    // Update is called once per frame
    void Update()
    {
        DoAnimationStuff();

        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir) {
        moveDirection = dir;
    }

    private void Destroy() {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CancelInvoke();
    }

    void ChangeAnimationState(string newState) {
        animator.Play(newState);
    }
}
