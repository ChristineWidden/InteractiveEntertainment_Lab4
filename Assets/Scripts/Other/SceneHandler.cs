using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;



public class SceneHandler : MonoBehaviour
{
    [SerializeField] GameObject fadeTransition;
    private SpriteRenderer sprite;
    private float currentValue;

    private PlayerInput playerInput;

    // SINGLETON PATTERN
    public static SceneHandler instance;

    private bool paused;
    public bool gameOver;

    private float waitBeforePause = 0;

    void Awake()
    {
        instance = gameObject.GetComponent<SceneHandler>();
    }

    void Start()
    {
        sprite = fadeTransition.GetComponent<SpriteRenderer>();
        
        sprite.color = new Color(0, 0, 0, 0);
        StartCoroutine(InterpolateFloat(1, 0, 0.5f));
        playerInput = GetComponent<PlayerInput>();
    }

    void Update()
    {
        sprite.color = new Color(0, 0, 0, currentValue);

        if (waitBeforePause >= 0)
        {
            waitBeforePause -= 1;
            return;
        }

        if (gameOver) {
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Additive);
            
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

    public void GameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void TransitionScene(string scene)
    {
        // TODO get transitions working
        InterpolateFloat(0, 1, 2f);
    }


    private IEnumerator InterpolateFloat(float startValue, float endValue, float duration)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the current value based on the interpolation progress
            currentValue = Mathf.Lerp(startValue, endValue, elapsedTime / duration);

            // Uncomment the line below if you want to use SmoothStep instead of Lerp
            // currentValue = Mathf.SmoothStep(startValue, endValue, elapsedTime / duration);

            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }
    }
}