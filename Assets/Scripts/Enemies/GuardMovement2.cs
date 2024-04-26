using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardMovement2 : MonoBehaviour
{

    private Physics physics;

    public int moveDirection = 1;

    public static int guardCount = 2;

    private bool frozen = false;

    void Start() {
        physics = gameObject.GetComponent<Physics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen)
        {
            physics.HInput = moveDirection;
            // transform.position = new Vector2(transform.position.x + moveDirection * moveSpeed * Time.deltaTime, transform.position.y);
        }
    }

    public void Freeze(float freezeTime)
    {
        frozen = true;
        StartCoroutine(UnFreeze(freezeTime));
    }
    IEnumerator UnFreeze(float freezeTime)
    {
        yield return new WaitForSeconds(freezeTime);
        frozen = false;
    }
}
