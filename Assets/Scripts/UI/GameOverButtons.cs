using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverButtons : MonoBehaviour
{
    [SerializeField] private string introScene;
    [SerializeField] private string creditsScene;

    public void MenuButtonClicked()
    {
        SceneManager.LoadScene(introScene);
    } 
    public void RetryButtonClicked()
    {
        SceneHandler.instance.ReloadLevel();
        
    }

}
