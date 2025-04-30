using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideAccessibility : IOptionObserver
{

    [SerializeField] List<GameObject> EnableWhenAccessibilityOn_A;
    [SerializeField] List<GameObject> EnableWhenAccessibilityOff_B;

    void Start() {
        SetEnabled();
    }

    public override void OnOptionChanged()
    {
        SetEnabled();
    }

    private void SetEnabled() {
        bool accessEnabled = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.ACCESSIBILITY_ENABLED);
        foreach (GameObject g in EnableWhenAccessibilityOn_A) {
            g.SetActive(accessEnabled);
        }
        foreach (GameObject g in EnableWhenAccessibilityOff_B) {
            g.SetActive(!accessEnabled);
        }
    }

    // Start is called before the first frame update

}
