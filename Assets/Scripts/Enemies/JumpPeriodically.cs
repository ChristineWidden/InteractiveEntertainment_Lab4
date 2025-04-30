using System.Collections;
using UnityEngine;

public class JumpPeriodically : MonoBehaviour
{
    [SerializeField] private float jumpInterval;
    private bool frozen = false;
    private float freezeTimer = 0;

    private Physics physics;

    void Start()
    {
        physics = GetComponent<Physics>();
        InvokeRepeating(nameof(Jump), 1.5f, jumpInterval);
    }

    void Update() {
        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
        } else if (frozen) {
            frozen = false;
        }
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
        freezeTimer = freezeTime;
    }
}
