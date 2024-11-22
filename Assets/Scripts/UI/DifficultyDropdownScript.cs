using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DifficultyDropdownScript : MonoBehaviour
{
    void Start()
    {
        TMP_Dropdown dropdown= GetComponent<TMP_Dropdown>();
        dropdown.value = OptionsManager.Instance.GetDifficulty();
    }

    public void HandleInputData(int val) {
        OptionsManager.Instance.SetDifficulty(val);
    }
}
