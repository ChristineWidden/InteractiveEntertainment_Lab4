using System.Collections.Generic;
using UnityEngine;
using System;

public enum BooleanOptionEnum
{
    MUSIC_MUTED,
    HIGH_CONTRAST_ON,
    EDGE_GUARD_ON,
    AUTO_FIRE_ON,
}

public enum MultiOptionEnum
{
    DIFFICULTY,
}

public enum DifficultyEnum
{
    STANDARD, EASY,
}

public class OptionsManager : MonoBehaviour
{
    // Singleton instance
    private static OptionsManager instance;

    public static OptionsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("OptionsManager").AddComponent<OptionsManager>();
                DontDestroyOnLoad(instance.gameObject);
                instance.SetDifficulty(0);
            }
            return instance;
        }
    }

    private float currentGameplaySpeed = 1f;
    public DifficultyOptions currentDifficulty;
    private DifficultyOptions standardDifficulty = new(DifficultyEnum.STANDARD);
    private DifficultyOptions easyDifficulty = new(DifficultyEnum.EASY);

    // List of observers
    private List<IOptionObserver> observers = new List<IOptionObserver>();

    // Boolean options
    [SerializeField] private bool musicMuted;
    [SerializeField] private bool highContrastOn;
    [SerializeField] private bool edgeGuardOn;
    [SerializeField] private bool autoFireOn;

    private void Awake()
    {
        // Ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);

        currentDifficulty = standardDifficulty;
    }


    private void OnEnable() {
        Debug.Log("Resetting gameplay speed to "+ currentGameplaySpeed);
        Time.timeScale = currentGameplaySpeed;
    }


    // Register an observer
    public void RegisterObserver(IOptionObserver observer)
    {
        if (!observers.Contains(observer))
            observers.Add(observer);
    }

    // Unregister an observer
    public void UnregisterObserver(IOptionObserver observer)
    {
        observers.Remove(observer);
    }

    // Call this method when options change
    private void NotifyObservers()
    {
        Debug.Log("Notifying observers of change");
        foreach (var observer in observers)
        {
            observer.OnOptionChanged();
        }
    }


    // Player prefs are for saving info between sessions
    // Figure out if I want to do that

    public void PublicNotifyObservers() {
        NotifyObservers();
    }

    public void SetBooleanOption(BooleanOptionEnum option, bool newValue) {
        switch (option)
        {
            case BooleanOptionEnum.MUSIC_MUTED:
                musicMuted = newValue;
                break;
            case BooleanOptionEnum.HIGH_CONTRAST_ON:
                highContrastOn = newValue;
                break;
            case BooleanOptionEnum.EDGE_GUARD_ON:
                edgeGuardOn = newValue;
                break;
            case BooleanOptionEnum.AUTO_FIRE_ON:
                autoFireOn = newValue;
                break;
            default:
                throw new Exception("No implemented behavior for option " + option);
        }
        Debug.Log(option + " VALUE NOW " +  newValue);
        NotifyObservers();
    }
    public bool GetBooleanOption(BooleanOptionEnum option) {
        return option switch
        {
            BooleanOptionEnum.MUSIC_MUTED => musicMuted,
            BooleanOptionEnum.HIGH_CONTRAST_ON => highContrastOn,
            BooleanOptionEnum.EDGE_GUARD_ON => edgeGuardOn,
            BooleanOptionEnum.AUTO_FIRE_ON => autoFireOn,
            _ => throw new Exception("No implemented behavior for option " + option),
        };
    }


    public void Pause() {
        Time.timeScale = 0;
    }
    public void Unpause() {
        Time.timeScale = currentGameplaySpeed;
    }

    public void SetGameplaySpeed(int val) {
        Debug.Log("SPEED " + val);
        switch (val) {
            case 0:
                currentGameplaySpeed = 1f;
                break;
            case 1:
                currentGameplaySpeed = 0.8f;
                break;
            case 2:
                currentGameplaySpeed = 0.6f;
                break;
        }
        Time.timeScale = currentGameplaySpeed;
    }

    public void SetDifficulty(int val) {
        Debug.Log("DIFFICULTY " + val);
        currentDifficulty = val switch
        {
            0 => standardDifficulty,
            1 => easyDifficulty,
            _ => standardDifficulty,
        };
    }

    public class DifficultyOptions {
        public float enemySpeedMultiplier; //check
        public float enemyDamageMultiplier; //check
        public float enemyImmunityFrameMultiplier; //check

        public float playerImmunityFrameMultiplier; // check
        public float playerProjectileFrequencyMultiplier; // check
        public float playerProjectileEffectDurationMultiplier; // TODO
        
        public float powerUpRespawnMultiplier; // TODO
        // public float gameplaySpeedMultiplier;

        public DifficultyOptions(DifficultyEnum difficultyEnum) {
            switch(difficultyEnum) {
                case DifficultyEnum.STANDARD:
                    InitStandard();
                    break;
                case DifficultyEnum.EASY:
                    InitEasy();
                    break;
                default:
                    InitStandard();
                    break;
            }
        }

        private void InitStandard() {
            enemySpeedMultiplier = 1;
            enemyDamageMultiplier = 1;
            // gameplaySpeedMultiplier = 1;
            playerImmunityFrameMultiplier = 1;
            enemyImmunityFrameMultiplier = 1;
            playerProjectileFrequencyMultiplier = 1;
            playerProjectileEffectDurationMultiplier = 1;
            powerUpRespawnMultiplier = 1;
        }

        private void InitEasy() {
            enemySpeedMultiplier = 0.5f;
            enemyDamageMultiplier = 0;
            // gameplaySpeedMultiplier = 1;
            playerImmunityFrameMultiplier = 2;
            enemyImmunityFrameMultiplier = 1;
            playerProjectileFrequencyMultiplier = 0.5f;
            playerProjectileEffectDurationMultiplier = 1;
            powerUpRespawnMultiplier = 1;
        }
    }

}
