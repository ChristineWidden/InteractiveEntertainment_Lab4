using System.Collections;
using UnityEngine;
using System;

public class GuardMovement2 : IOptionObserver
{

    private Physics physics;

    public int moveDirection = 1;

    public static int guardCount = 2;

    private bool frozen = false;
    private float freezeTimer = 0;

    void Start() {
        physics = gameObject.GetComponent<Physics>();
    }

    void Update()
    {
        if (freezeTimer > 0)
        {
            freezeTimer -= Time.deltaTime;
        } else if (frozen) {
            frozen = false;
        }

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
        freezeTimer = freezeTime;
    }


    public override void OnOptionChanged()
    {
        OptionsManager optionsManager = OptionsManager.Instance != null ? OptionsManager.Instance : throw new ArgumentNullException("Options manager was null");
        physics.setSpeedMultiplier(optionsManager.currentDifficulty.enemySpeedMultiplier);
        physics.UpdateDifficulty();
    }
}
