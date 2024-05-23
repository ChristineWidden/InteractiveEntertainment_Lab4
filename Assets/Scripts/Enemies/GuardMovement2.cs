using System.Collections;
using UnityEngine;

public class GuardMovement2 : MonoBehaviour
{

    private Physics physics;

    public int moveDirection = 1;

    public static int guardCount = 2;

    private bool frozen = false;

    void Start() {
        physics = gameObject.GetComponent<Physics>();
    }

    void Update()
    {
        if (!frozen)
        {
            physics.HInput = moveDirection;
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
