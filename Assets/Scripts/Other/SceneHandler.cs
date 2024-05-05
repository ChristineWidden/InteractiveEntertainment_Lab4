using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    [SerializeField] GameObject fadeTransition;
    private SpriteRenderer sprite;
    private float currentValue;


    // SINGLETON PATTERN
    public static GameObject instance;

    void Awake()
    {
        instance = gameObject;
    }

    void Start() {
        sprite = fadeTransition.GetComponent<SpriteRenderer>();
        sprite.material.SetFloat("_Alpha", 1);
        InterpolateFloat(1, 0, 2f);
    }

    void Update() {
        sprite.material.SetFloat("_Alpha", currentValue);
    }

    public void GameOver() {

    }

    public void TransitionScene(string scene) {
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