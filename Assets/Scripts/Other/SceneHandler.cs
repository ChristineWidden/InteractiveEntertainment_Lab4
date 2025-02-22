using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class SceneHandler : MonoBehaviour
{

    private PlayerInput playerInput;

    // SINGLETON PATTERN
    public static SceneHandler instance;

    private bool paused;
    public bool gameOver;
    private bool gameOverLoaded = false;

    private float waitBeforePause = 0;

    void Awake()
    {
        instance = gameObject.GetComponent<SceneHandler>();
    }

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }


    void Update()
    {

        if (waitBeforePause >= 0)
        {
            waitBeforePause -= 1;
            return;
        }

        if (gameOver && !gameOverLoaded) {
            gameOverLoaded = true;
            SceneManager.LoadScene("Game Over", LoadSceneMode.Additive);
            
            paused = true;
            OptionsManager.Instance.Pause();
        }

        if (playerInput.actions["Pause"].ReadValue<float>() <= 0.5f)
        {
            return;
        }

        if (!paused)
        {
            SceneManager.LoadScene("Pause Menu", LoadSceneMode.Additive);
            paused = true;
            OptionsManager.Instance.Pause();
        }
        else
        {
            Unpause();
        }
        waitBeforePause = 50;
    }

    public void Unpause()
    {
        paused = false;
        SceneManager.UnloadSceneAsync("Pause Menu");
        OptionsManager.Instance.Unpause();
    }

    public void ReloadLevel()
    {
        gameOver = false;
        gameOverLoaded = false;
        SceneManager.UnloadSceneAsync("Game Over");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        OptionsManager.Instance.Unpause();
        paused = false;
    }

}