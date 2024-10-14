using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class WinController : MonoBehaviour
{
    private UIDocument _doc;
    private Button _creditsButton2;
    private Button _mainMenuButton2;
    private Button _exitButton2;
    private VisualElement _buttonsWrapper;

    private void Awake(){
        _doc = GetComponent<UIDocument>();

        _creditsButton2 = _doc.rootVisualElement.Q<Button>("CreditsButton2");
        _creditsButton2.clicked += CreditsButtonClicked2;

        _mainMenuButton2 = _doc.rootVisualElement.Q<Button>("MainMenuButton2");
        _mainMenuButton2.clicked += MainMenuButtonClicked2;

        _exitButton2 = _doc.rootVisualElement.Q<Button>("ExitButton2");
        _exitButton2.clicked += ExitButtonClicked2;
    }

    private void CreditsButtonClicked2(){
        SceneManager.LoadScene("Credits");
    }

    private void MainMenuButtonClicked2(){
        SceneManager.LoadScene("Menu");
    }

    private void ExitButtonClicked2(){
        Application.Quit();
        Debug.Log("Quitting");
    }

}
