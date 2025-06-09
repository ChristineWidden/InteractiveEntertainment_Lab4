using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTraceAlert : MonoBehaviour
{
    [SerializeField] private AudioSource alertAudioSource;
    [SerializeField] private AudioClip alertSound;
    [SerializeField] private Rigidbody2D goblin;
    [SerializeField] private BoxCollider2D thisCollider;

    [SerializeField] private  float maxVolume = 1.0f;   // Max volume when enemies are very close
    [SerializeField] private  float minVolume = 0.1f;   // Minimum volume when no enemies are near
    [SerializeField] private  float pitchVariation = 0.2f; // How much pitch changes based on closeness
    [SerializeField] private  float maxPanningDistance = 10f; // Distance where full panning occurs

    private List<Transform> nearbyTargets = new();  // Active enemies in range

    private bool inPlayerProximity;

    // TODO this isn't working. Fix it.

    void Start() {
        thisCollider = GetComponent<BoxCollider2D>();
        alertAudioSource.clip = alertSound;
        alertAudioSource.loop = true;
        alertAudioSource.volume = 0f;  // Start silent
        alertAudioSource.spatialBlend = 0f; // Ensure it's a 2D stereo sound
        alertAudioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        // if (inPlayerProximity && dangerCollider) {
        //     float distance = Math.Abs(dangerCollider.attachedRigidbody.position.x - goblin.position.x);
        //     float volume = (thisCollider.size.x - Math.Clamp(distance, 0, thisCollider.size.x)) / thisCollider.size.x;

        //     SoundEffectHolder.instance.SetProximityAlertVolume(volume);
        // }

        if (!OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.AUDIO_NAVIGATION_ON)) {
            alertAudioSource.volume = 0;
            return;
        }

        if (nearbyTargets.Count == 0)
        {
            alertAudioSource.volume = Mathf.Lerp(alertAudioSource.volume, 0f, Time.deltaTime * 5f);
            alertAudioSource.panStereo = 0f; // Reset panning to center when no enemies are nearby
            return;
        }

        float totalVolume = 0f;
        float closestDistance = float.MaxValue;
        Transform closestTarget = null;
        Transform player = transform;

        foreach (Transform targetTransform in nearbyTargets)
        {
            if (targetTransform == null) continue;

            float distance = Vector3.Distance(player.position, targetTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTarget = targetTransform;
            }

            float normalizedDistance = Mathf.InverseLerp(maxPanningDistance, 0f, distance);
            totalVolume += Mathf.Lerp(minVolume, maxVolume, normalizedDistance);
        }

        // Apply stereo panning based on enemy position
        if (closestTarget != null)
        {   
            float rootVal = 4f; // power value to adjust rate of volume/pitch increase

            float normalizedDistance = (float) Math.Pow(Mathf.InverseLerp(maxPanningDistance, 0f, closestDistance), rootVal);
            alertAudioSource.volume = Mathf.Lerp(minVolume, maxVolume, normalizedDistance);

            // Adjust pitch slightly based on distance
            float pitchFactor = (float) Math.Pow(Mathf.InverseLerp(maxPanningDistance, 0f, closestDistance), rootVal);;
            alertAudioSource.pitch = 1.0f + (pitchFactor * pitchVariation);

            // Stereo panning
            Vector3 direction = closestTarget.transform.position - player.position;
            float panValue = Mathf.Clamp(direction.x / maxPanningDistance, -1f, 1f);
            alertAudioSource.panStereo = panValue;
        }
    }

    public void enterPlayerProximity(Collider2D other) {
        if (!nearbyTargets.Contains(other.transform))
        {
            nearbyTargets.Add(other.transform);
        }
    }
    public void exitPlayerProximity(Collider2D other) {
        nearbyTargets.Remove(other.transform);
    }

    // public void PlayEnemyAheadAlert(Collider2D other) {
    //     dangerCollider = other;
    //     SoundEffectHolder.instance.PlayProximityAlert(enemyAheadSound);
    // }

    // public void PlayDangerAlert(Collider2D other) {
    //     dangerCollider = other;
    //     SoundEffectHolder.instance.PlayProximityAlert(alertSound);

    //     Vector3 direction = closestEnemy.transform.position - player.position;
    //     float panValue = Mathf.Clamp(direction.x / maxPanningDistance, -1f, 1f);
    // }
}
