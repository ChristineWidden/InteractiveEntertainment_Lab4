using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class ToggleController : MonoBehaviour
{
    private Toggle toggle; // Reference to the Toggle UI element
    [SerializeField] private BooleanOptionEnum associatedOption;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        toggle.isOn = GetOptionValue(associatedOption);
    }

    bool GetOptionValue(BooleanOptionEnum associatedOption)
    {
        return OptionsManager.Instance.GetBooleanOption(associatedOption);
    }

    // This method is called when the toggle value changes
    public void OnToggleValueChanged()
    {
        OptionsManager.Instance.SetBooleanOption(associatedOption, toggle.isOn);
    }
}
