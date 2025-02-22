using System.Collections;
using UnityEngine;
using System;

public class GuardMovement2 : IOptionObserver
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
        } else {
            physics.HInput = 0;
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


    public override void OnOptionChanged()
    {
        OptionsManager optionsManager = OptionsManager.Instance != null ? OptionsManager.Instance : throw new ArgumentNullException("Options manager was null");
        physics.setSpeedMultiplier(optionsManager.currentDifficulty.enemySpeedMultiplier);
        physics.UpdateDifficulty();
    }
}
