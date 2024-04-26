using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BooleanOptionEnum
{
    MUSIC_MUTED,
    HIGH_CONTRAST_ON,
}

public enum DifficultyEnum
{
    STANDARD,
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
            }
            return instance;
        }
    }

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
    }


    // List of observers
    private List<IOptionObserver> observers = new List<IOptionObserver>();

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
        foreach (var observer in observers)
        {
            observer.OnOptionChanged();
        }
    }

    // Update options and notify observers
    // public void UpdateOptions()
    // {
    //     // Update options...

    //     // Notify observers about the change
    //     NotifyObservers();
    // }




    [SerializeField] private bool musicMuted;
    [SerializeField] private bool highContrastOn;

    // Player prefs are for saving info between sessions
    // Figure out if I want to do that

    public void SetBooleanOption(BooleanOptionEnum option, bool newValue) {
        switch (option)
        {
            case BooleanOptionEnum.MUSIC_MUTED:
                musicMuted = newValue;
                break;
            case BooleanOptionEnum.HIGH_CONTRAST_ON:
                highContrastOn = newValue;
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
            _ => throw new Exception("No implemented behavior for option " + option),
        };
    }

    public DifficultyOptions currentDifficulty;
    private readonly DifficultyOptions standardDifficulty = new(DifficultyEnum.STANDARD);
    public void SetDifficulty(DifficultyEnum difficulty) {
        currentDifficulty = difficulty switch
        {
            DifficultyEnum.STANDARD => standardDifficulty,
            _ => standardDifficulty,
        };
    }

    public class DifficultyOptions {
        public float enemySpeedMultiplier; //check
        public float enemyDamageMultiplier; //check
        public float enemyImmunityFrameMultiplier;

        public float playerImmunityFrameMultiplier;
        public float playerProjectileFrequencyMultiplier;
        public float playerProjectileEffectDurationMultiplier;
        
        public float powerUpRespawnMultiplier;
        public float gameplaySpeedMultiplier;

        public DifficultyOptions(DifficultyEnum difficultyEnum) {
            switch(difficultyEnum) {
                case DifficultyEnum.STANDARD:
                    InitStandard();
                    break;
                default:
                    InitStandard();
                    break;
            }
        }

        private void InitStandard() {
            enemySpeedMultiplier = 1;
            enemyDamageMultiplier = 1;
            gameplaySpeedMultiplier = 1;
            playerImmunityFrameMultiplier = 1;
            enemyImmunityFrameMultiplier = 1;
            playerProjectileFrequencyMultiplier = 1;
            playerProjectileEffectDurationMultiplier = 1;
            powerUpRespawnMultiplier = 1;
        }
    }

}
