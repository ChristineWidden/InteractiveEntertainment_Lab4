using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpPeriodically : MonoBehaviour
{
    [SerializeField] private float jumpInterval;
    private bool frozen = false;

    private Physics physics;

    void Start()
    {
        physics = GetComponent<Physics>();
        InvokeRepeating(nameof(Jump), 1.5f, jumpInterval);
    }

    private void Jump()
    {
        if (!frozen)
        {
            StartCoroutine(Jump(0.2f));
        }
    }
    IEnumerator Jump(float releaseJump)
    {
        physics.JumpInput = 1;
        yield return new WaitForSeconds(releaseJump);
        physics.JumpInput = 0;
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
