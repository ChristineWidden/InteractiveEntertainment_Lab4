using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://docs.unity3d.com/ScriptReference/PlayerPrefs.html

public class OptionsController : MonoBehaviour
{
    private void Awake(){
        //PlayerPrefs.SetInt("MUTED", 0);
        //PlayerPrefs.SetInt("HIGH CONTRAST", 0);

        //PlayerPrefs.Save();
    }

    public void MuteButtonClicked(){
        // int muteState = PlayerPrefs.GetInt("MUTED");
        // PlayerPrefs.SetInt("MUTED", muteState == 0 ? 1 : 0); // Switch mute state
        // PlayerPrefs.Save();

        //bool muteState = OptionsManager.Instance.GetMute();
        //OptionsManager.Instance.SetMute(!muteState);
    }

}
