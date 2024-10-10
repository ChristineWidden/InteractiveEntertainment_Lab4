using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class OptionsDoneButton : MonoBehaviour {
    public void OnClick() {
        OptionsManager.Instance.PublicNotifyObservers();
    }
}