using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    private UIDocument _doc;
    private Button _restartButton;
    private Button _mainMenuButton;
    private Button _exitButton;
    private VisualElement _buttonsWrapper;

    private void Awake(){
        _doc = GetComponent<UIDocument>();

        _restartButton = _doc.rootVisualElement.Q<Button>("RestartButton");
        _restartButton.clicked += RestartButtonClicked;

        _mainMenuButton = _doc.rootVisualElement.Q<Button>("MainMenuButton");
        _mainMenuButton.clicked += MainMenuButtonClicked;

        _exitButton = _doc.rootVisualElement.Q<Button>("ExitButton");
        _exitButton.clicked += ExitButtonClicked;
    }

    private void RestartButtonClicked(){
        SceneManager.LoadScene("FirstLevel");
    }

    private void MainMenuButtonClicked(){
        SceneManager.LoadScene("Menu");
    }

    private void ExitButtonClicked(){
        Application.Quit();
        Debug.Log("Quitting");
    }

}
