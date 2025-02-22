using UnityEngine;
using System.Collections.Generic;
using System;

public class PitAudioCollider : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource pitSoundAudioSource;  // Single AudioSource for all enemies
    [SerializeField] private  float maxVolume = 1.0f;   // Max volume when enemies are very close
    [SerializeField] private  float minVolume = 0.1f;   // Minimum volume when no enemies are near
    [SerializeField] private  float maxPanningDistance = 10f; // Distance where full panning occurs

    private List<Transform> nearbyPits = new();  // Active enemies in range

    [SerializeField] private float rootVal;

    private void Start()
    {
        pitSoundAudioSource = SoundEffectHolder.instance.Environment;
        SoundEffectHolder.instance.Environment.volume = 0f;
        SoundEffectHolder.instance.PlayClip(SoundEffectHolder.instance.Environment, audioClip);
        pitSoundAudioSource.Play();
    }

    private void Update()
    {
        if (nearbyPits.Count == 0)
        {
            // todo fix how volume is managed here
            pitSoundAudioSource.volume = 0;
            return;
        }

        float totalVolume = 0f;
        float closestDistance = float.MaxValue;
        Transform closestPit = null;
        Transform player = transform;

        foreach (Transform pitTransform in nearbyPits)
        {
            float distance = Vector3.Distance(player.position, pitTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPit = pitTransform;
            }

            float normalizedDistance = Mathf.InverseLerp(maxPanningDistance, 0f, distance);
            totalVolume += Mathf.Lerp(minVolume, maxVolume, normalizedDistance);
        }

        // Apply stereo panning based on enemy position
        if (closestPit != null)
        {
            float normalizedDistance = Mathf.InverseLerp(maxPanningDistance, 0f, closestDistance);
            pitSoundAudioSource.volume = (float) Math.Pow(Mathf.Lerp(minVolume, maxVolume, normalizedDistance), rootVal);

            // Stereo panning
            Vector3 direction = closestPit.transform.position - player.position;
            float panValue = Mathf.Clamp(direction.x / maxPanningDistance, -1f, 1f);
            pitSoundAudioSource.panStereo = panValue;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy") && !nearbyPits.Contains(other.transform))
        {
            nearbyPits.Add(other.transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            nearbyPits.Remove(other.transform);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pit") && !nearbyPits.Contains(other.transform))
        {
            nearbyPits.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Pit"))
        {
            nearbyPits.Remove(other.transform);
        }
    }
}
