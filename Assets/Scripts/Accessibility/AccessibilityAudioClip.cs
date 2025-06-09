using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessibilityAudioClip : MonoBehaviour
{

    [SerializeField] private AudioClip A;
    [SerializeField] private AudioClip B;

    public void PlayClip()
    {
        Debug.Log("Playing accessibility clip");    
        bool accessEnabled = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.ACCESSIBILITY_ENABLED);
        if (accessEnabled)
        {
            SoundEffectHolder.instance.PlayClip(
                                SoundEffectHolder.instance.Narration,
                                A);
        }
        else
        {
            SoundEffectHolder.instance.PlayClip(
                    SoundEffectHolder.instance.Narration,
                    B);
        }

    }
}
