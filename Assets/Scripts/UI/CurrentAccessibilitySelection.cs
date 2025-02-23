using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CurrentAccessibilitySelection : MonoBehaviour
{

    private TextMeshProUGUI text;

    private void OnEnable()
    {
        text = GetComponent<TextMeshProUGUI>();
        Debug.Log(text);
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
