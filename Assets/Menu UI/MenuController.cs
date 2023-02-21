using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private UIDocument _doc;
    private Button _playButton;
    private Button _muteButton;
    private Button _creditsButton;
    private Button _exitButton;
    private VisualElement _buttonsWrapper;

    private bool _muted = false;

    private void Awake(){
        _doc = GetComponent<UIDocument>();

        _playButton = _doc.rootVisualElement.Q<Button>("PlayButton");
        _playButton.clicked += PlayButtonClicked;

        _buttonsWrapper = _doc.rootVisualElement.Q<VisualElement>("Buttons");
        _muteButton = _doc.rootVisualElement.Q<Button>("MuteButton");
        _muteButton.clicked += MuteButtonClicked;

        _creditsButton = _doc.rootVisualElement.Q<Button>("CreditsButton");
        _creditsButton.clicked += CreditsButtonClicked;

        _exitButton = _doc.rootVisualElement.Q<Button>("ExitButton");
        _exitButton.clicked += ExitButtonClicked;
    }

    private void PlayButtonClicked(){
        SceneManager.LoadScene("FirstLevel");
    }

    private void ExitButtonClicked(){
        Application.Quit();
        Debug.Log("Quitting");
    }

    private void MuteButtonClicked(){
        _muted = !_muted;
        AudioListener.volume = _muted ? 0 : 1;
    }

    private void CreditsButtonClicked(){
        SceneManager.LoadScene("Credits");
    }

    private void BackButtonClicked(){
        _buttonsWrapper.Clear();
        _buttonsWrapper.Add(_playButton);
        _buttonsWrapper.Add(_muteButton);
        _buttonsWrapper.Add(_creditsButton);
        _buttonsWrapper.Add(_exitButton);
    }
}
