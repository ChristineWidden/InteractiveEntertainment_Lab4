using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class DropdownScript : MonoBehaviour
{
    // public delegate void DropdownEvent(int index);
    public UnityEvent<int> onValueChanged;

    public void HandleInputData(int val) {
        // OptionsManager.Instance.SetDifficulty(val);
        onValueChanged.Invoke(val);
    }
}
