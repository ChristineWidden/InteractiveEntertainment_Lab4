using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private PowerUp powerUpPrefab;
    private PowerUp powerUp;
    [SerializeField] private float respawnTime;

    // Start is called before the first frame update
    void Start()
    {
        powerUp = InstantiatePowerUp();
        powerUp.destroyedEvent.AddListener(PowerUpDestroyed);
    }

    void PowerUpDestroyed() {
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
