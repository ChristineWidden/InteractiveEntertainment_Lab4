using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private string playLevel;
    [SerializeField] private string creditsScene;

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene(playLevel);
    }

    public void MainMenuButtonClicked()
    {
        SceneHandler.instance.Unpause();
        SceneManager.LoadScene("Menu");
    }

    public void ExitButtonClicked()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }

    public void CreditsButtonClicked()
    {
        SceneHandler.instance.Unpause();
        SceneManager.LoadScene(creditsScene);
    }

    public void BackToGameButtonClicked()
    {
        SceneHandler.instance.Unpause();
    }

}
