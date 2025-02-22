using System;
using UnityEngine;
using UnityEngine.Events;

public class CrossSceneCommunicator : MonoBehaviour
{
    // Singleton instance
    private static CrossSceneCommunicator instance;

    public static string currentLevel;

    public static CrossSceneCommunicator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CrossSceneCommunicator").AddComponent<CrossSceneCommunicator>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }
    // [SerializeField] private UnityEvent<PowerUpEnum> updatePowerUpDescription;
    // [SerializeField] private UnityEvent<Sprite> updatePowerUpSprite;

    public PowerUpEnum powerUp;
    public Sprite powerUpSprite;


}
