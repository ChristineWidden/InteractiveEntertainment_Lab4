using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


public class DropdownScript : MonoBehaviour
{
    public UnityEvent<int> onValueChanged;

    public void HandleInputData(int val) {
        onValueChanged.Invoke(val);
    }
}
