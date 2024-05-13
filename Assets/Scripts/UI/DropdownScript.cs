using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class GameplaySpeedDropdownScript : MonoBehaviour
{

    public void HandleInputData(int val) {
        OptionsManager.Instance.SetGameplaySpeed(val);
    }
}
