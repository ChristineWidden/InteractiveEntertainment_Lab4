using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CurrentAccessibilitySelection : MonoBehaviour
{

    private Text text;

    private void OnEnable()
    {
        text = GetComponent<Text>();
        CurrentSelectionManager.Instance.selectionChangedEvent.AddListener(UpdateText);
    }

    void UpdateText(GameObject selectedObj) {
        if (selectedObj != null)
        {
            Debug.Log("Currently selected UI element: " + selectedObj.name);
            selectedObj.TryGetComponent<AccessibilityOptionDescription>(out AccessibilityOptionDescription description);
            if (description != null) {
                text.text = description.GetDescription();
            } else {
                text.text = "no description for this object";
            }
        }
    }
}
