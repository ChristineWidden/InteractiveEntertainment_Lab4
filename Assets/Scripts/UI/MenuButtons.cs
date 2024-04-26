using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{

    public void PlayButtonClicked(){
        // SceneManager.LoadScene("Intro Cutscene");
        SceneManager.LoadScene("FirstLevel 1");
    }

    public void ExitButtonClicked(){
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void CreditsButtonClicked(){
        SceneManager.LoadScene("Credits");
    }

}
