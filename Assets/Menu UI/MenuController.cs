using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private UIDocument _doc;
    private Button _playButton;
    private Button _settingsButton;
    private Button _creditsButton;
    private Button _exitButton;
    private VisualElement _buttonsWrapper;

    [SerializeField]
    private VisualTreeAsset _settingsButtonsTemplate;
    private VisualElement _settingsButtons;

    private void Awake(){
        _doc = GetComponent<UIDocument>();

        _playButton = _doc.rootVisualElement.Q<Button>("PlayButton");
        _playButton.clicked += PlayButtonClicked;

        _buttonsWrapper = _doc.rootVisualElement.Q<VisualElement>("Buttons");
        _settingsButton = _doc.rootVisualElement.Q<Button>("SettingsButton");
        _settingsButton.clicked += SettingsButtonClicked;
        _settingsButtons =_settingsButtonsTemplate.CloneTree();

        _creditsButton = _doc.rootVisualElement.Q<Button>("CreditsButton");

        _exitButton = _doc.rootVisualElement.Q<Button>("ExitButton");
        _exitButton.clicked += ExitButtonClicked;

        var backButton = _settingsButtons.Q<Button>("BackButton");
        backButton.clicked += BackButtonClicked;
    }

    private void PlayButtonClicked(){
        SceneManager.LoadScene("FirstLevel");
    }

    private void ExitButtonClicked(){
        Application.Quit();
        Debug.Log("Quitting");
    }

    private void SettingsButtonClicked(){
        _buttonsWrapper.Clear();
        _buttonsWrapper.Add(_settingsButtons);
    }

    private void BackButtonClicked(){
        _buttonsWrapper.Clear();
        _buttonsWrapper.Add(_playButton);
        _buttonsWrapper.Add(_settingsButton);
        _buttonsWrapper.Add(_creditsButton);
        _buttonsWrapper.Add(_exitButton);
    }
}
