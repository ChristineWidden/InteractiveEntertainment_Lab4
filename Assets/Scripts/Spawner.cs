using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PowerUp powerUpPrefab;
    [SerializeField] private AudioClip powerupCollectSound;
    [SerializeField] private AudioClip powerupDescription;
    private PowerUp powerUp;
    [SerializeField] private float respawnTime;

    // Start is called before the first frame update
    void Start()
    {
        powerUp = InstantiatePowerUp();
        powerUp.destroyedEvent.AddListener(PowerUpDestroyed);
    }

    void PowerUpDestroyed() {
        SoundEffectHolder.instance.SoundEffect.clip = powerupCollectSound;
        SoundEffectHolder.instance.SoundEffect.Play();
        SoundEffectHolder.instance.Narration.clip = powerupDescription;
        SoundEffectHolder.instance.Narration.Play();
        StartCoroutine(SpawnNewPowerUp(respawnTime));
    }
    
    IEnumerator SpawnNewPowerUp(float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        powerUp = InstantiatePowerUp();
        powerUp.destroyedEvent.AddListener(PowerUpDestroyed);
    }

    PowerUp InstantiatePowerUp() {
        return Instantiate(powerUpPrefab, transform.position, Quaternion.identity, transform);
    }
}
