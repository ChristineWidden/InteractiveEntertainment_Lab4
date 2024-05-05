using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private string introScene;
    [SerializeField] private string creditsScene;

    public void PlayButtonClicked(){
        SceneManager.LoadScene(introScene);
    }

    public void ExitButtonClicked(){
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void CreditsButtonClicked(){
        SceneManager.LoadScene(creditsScene);
    }

}
