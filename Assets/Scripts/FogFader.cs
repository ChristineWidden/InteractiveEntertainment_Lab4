using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFader : MonoBehaviour
{
    private SpriteRenderer sprite;
    private float currentValue;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("FOG ENABLED");
        sprite = GetComponent<SpriteRenderer>();
        
        sprite.color = new Color(0, 0, 0, 0);
        StartCoroutine(InterpolateFloat(1, 0, 0.5f));
    }

    // Update is called once per frame
    void Update()
    {
        sprite.color = new Color(0, 0, 0, currentValue);
    }

    public void TransitionScene(string scene)
    {
        StartCoroutine(
                InterpolateFloat(0, 1, 2f));
    }

    private IEnumerator InterpolateFloat(float startValue, float endValue, float duration)
    {
        Debug.Log("FADING FOG");
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
        currentValue = endValue;
    }
}
