using UnityEngine;
using System.Collections.Generic;

public class EnemyAudioManager : MonoBehaviour
{
    private AudioSource enemySoundAudioSource;  // Single AudioSource for all enemies
    [SerializeField] private  float maxVolume = 1.0f;   // Max volume when enemies are very close
    [SerializeField] private  float minVolume = 0.1f;   // Minimum volume when no enemies are near
    [SerializeField] private  float pitchVariation = 0.2f; // How much pitch changes based on closeness
    [SerializeField] private  float maxPanningDistance = 10f; // Distance where full panning occurs

    private List<Transform> nearbyEnemies = new();  // Active enemies in range

    private void Start()
    {
        enemySoundAudioSource = SoundEffectHolder.instance.EnemyFootsteps;
        SoundEffectHolder.instance.EnemyFootsteps.volume = 0f;
        enemySoundAudioSource.Play();
    }

    private void Update()
    {
        if (nearbyEnemies.Count == 0)
        {
            // todo fix how volume is managed here
            enemySoundAudioSource.volume = Mathf.Lerp(enemySoundAudioSource.volume, 0f, Time.deltaTime * 5f);
            enemySoundAudioSource.panStereo = 0f; // Reset panning to center when no enemies are nearby
            return;
        }

        // float totalVolume = 0f;
        float closestDistance = float.MaxValue;
        Enemy closestEnemy = null;
        Transform player = transform;

        foreach (Transform enemyTransform in nearbyEnemies)
        {
            Enemy enemy = enemyTransform.GetComponent<Enemy>();
            if (enemy == null) continue;

            float distance = Vector3.Distance(player.position, enemyTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }

            // float normalizedDistance = Mathf.InverseLerp(maxPanningDistance, 0f, distance);
            // totalVolume += Mathf.Lerp(minVolume, maxVolume, normalizedDistance);
        }

        // Apply stereo panning based on enemy position
        if (closestEnemy != null)
        {
            // If the audio clip isn't already set to the closest enemy's footsteps, switch it
            if (enemySoundAudioSource.clip != closestEnemy.getFootsteps())
            {
                enemySoundAudioSource.clip = closestEnemy.getFootsteps();
                enemySoundAudioSource.Play(); // Restart the loop with new sound
            }

            float normalizedDistance = Mathf.InverseLerp(maxPanningDistance, 0f, closestDistance);
            enemySoundAudioSource.volume = Mathf.Lerp(minVolume, maxVolume, normalizedDistance);

            // Adjust pitch slightly based on distance
            float pitchFactor = Mathf.InverseLerp(maxPanningDistance, 0f, closestDistance);
            enemySoundAudioSource.pitch = 1.0f + (pitchFactor * pitchVariation);

            // Stereo panning
            Vector3 direction = closestEnemy.transform.position - player.position;
            float panValue = Mathf.Clamp(direction.x / maxPanningDistance, -1f, 1f);
            enemySoundAudioSource.panStereo = panValue;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy") && !nearbyEnemies.Contains(other.transform))
        {
            nearbyEnemies.Add(other.transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag("Enemy"))
        {
            nearbyEnemies.Remove(other.transform);
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !nearbyEnemies.Contains(other.transform))
        {
            nearbyEnemies.Add(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            nearbyEnemies.Remove(other.transform);
        }
    }
}
