using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public enum BooleanOptionEnum
{
    ACCESSIBILITY_ENABLED,
    NARRATION_MUTED,
    HIGH_CONTRAST_ON,
    LARGER_POWERUPS_ON,
    LARGER_PROJECTILES_ON,
    EDGE_GUARD_ON,
    AUTO_FIRE_ON,
    AUTO_JUMP_ON,
    SPRITE_OUTLINES_ON,
    SMALL_HEALTH_RING,
    AUDIO_NAVIGATION_ON,
    ENVIRONMENT_PROXIMITY_ON,
}

public enum MultiOptionEnum
{
    DIFFICULTY,
}

public enum DifficultyEnum
{
    STANDARD, EASY, HARD, INVULNERABLE
}

public class OptionsManager : MonoBehaviour
{
    // Singleton instance
    private static OptionsManager instance;
    private bool versionLoadedYet = false;

    public static OptionsManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("OptionsManager").AddComponent<OptionsManager>();
                DontDestroyOnLoad(instance.gameObject);

                // UnityEngine.Random.InitState((int)System.DateTime.UtcNow.Ticks); 
                // instance.accessibilityEnabled = UnityEngine.Random.value >= 0.5f;
                // System.Random rand = new System.Random(Guid.NewGuid().GetHashCode());
                int seed = (int)System.DateTime.Now.Ticks;
                Debug.Log("SEED: " + seed);
                System.Random rand = new System.Random();
                double randval = rand.NextDouble();
                Debug.Log("RANDVAL: " + randval);
                instance.accessibilityEnabled = randval >= 0.5;

                instance.SetDifficulty(0);
            }
            return instance;
        }
    }

    // private static int GenerateSeed()
    // {
    //     int timeSeed = (int)DateTime.UtcNow.Ticks; // Time-based seed
    //     int frameSeed = Time.frameCount; // Frame count
    //     int mouseSeed = (int)(Input.mousePosition.x + Input.mousePosition.y); // Mouse position
    //     int browserSeed = Application.productName.GetHashCode(); // Browser/game-specific info

    //     return timeSeed ^ frameSeed ^ mouseSeed ^ browserSeed; // XOR for a unique seed
    // }

    private float defaultGameplaySpeed = 1f;
    private int gameplaySpeedVal = 0;
    public DifficultyOptions currentDifficulty;
    private DifficultyOptions standardDifficulty = new(DifficultyEnum.STANDARD);
    private DifficultyOptions easyDifficulty = new(DifficultyEnum.EASY);
    private DifficultyOptions hardDifficulty = new(DifficultyEnum.HARD);
    private DifficultyOptions invulnerableDifficulty = new(DifficultyEnum.INVULNERABLE);
    public bool IsPaused { get; private set; }

    [SerializeField] private AudioMixer mixer;

    // List of observers
    private List<IOptionObserver> observers = new List<IOptionObserver>();

    // Boolean options
    [SerializeField] private bool accessibilityEnabled;
    [SerializeField] private bool musicMuted;
    [SerializeField] private bool highContrastOn;
    [SerializeField] private bool largerPowerupsOn;
    [SerializeField] private bool largerProjectilesOn;
    [SerializeField] private bool edgeGuardOn;
    [SerializeField] private bool autoFireOn;
    [SerializeField] private bool autoJumpOn;
    [SerializeField] private bool spriteOutlinesOn;
    [SerializeField] private bool smallHealthRing;
    [SerializeField] private bool audioNavigationOn;
    [SerializeField] private bool environmentProximityOn;

    private void Awake()
    {
        if (!versionLoadedYet)
        {
            versionLoadedYet = true;

        }

        // Ensure only one instance exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        int seed = (int)System.DateTime.Now.Ticks;
        Debug.Log("SEED: " + seed);
        System.Random rand = new System.Random();
        double randval = rand.NextDouble();
        Debug.Log("RANDVAL: " + randval);
        instance.accessibilityEnabled = randval >= 0.5;
        
        DontDestroyOnLoad(gameObject);

        currentDifficulty = standardDifficulty;
    }

    private void OnEnable()
    {
        Debug.Log("Resetting gameplay speed to " + defaultGameplaySpeed);
        Time.timeScale = defaultGameplaySpeed;
        IsPaused = false;
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

    public void PublicNotifyObservers()
    {
        NotifyObservers();
    }

    public void SetBooleanOption(BooleanOptionEnum option, bool newValue)
    {
        switch (option)
        {
            case BooleanOptionEnum.ACCESSIBILITY_ENABLED:
                accessibilityEnabled = newValue;
                break;
            case BooleanOptionEnum.NARRATION_MUTED:
                musicMuted = newValue;
                break;
            case BooleanOptionEnum.HIGH_CONTRAST_ON:
                highContrastOn = newValue;
                break;
            case BooleanOptionEnum.LARGER_POWERUPS_ON:
                largerPowerupsOn = newValue;
                break;
            case BooleanOptionEnum.LARGER_PROJECTILES_ON:
                largerProjectilesOn = newValue;
                break;
            case BooleanOptionEnum.EDGE_GUARD_ON:
                edgeGuardOn = newValue;
                break;
            case BooleanOptionEnum.AUTO_FIRE_ON:
                autoFireOn = newValue;
                break;
            case BooleanOptionEnum.AUTO_JUMP_ON:
                autoJumpOn = newValue;
                break;
            case BooleanOptionEnum.SPRITE_OUTLINES_ON:
                spriteOutlinesOn = newValue;
                break;
            case BooleanOptionEnum.SMALL_HEALTH_RING:
                smallHealthRing = newValue;
                break;
            case BooleanOptionEnum.AUDIO_NAVIGATION_ON:
                audioNavigationOn = newValue;
                break;
            case BooleanOptionEnum.ENVIRONMENT_PROXIMITY_ON:
                environmentProximityOn = newValue;
                break;
            default:
                throw new Exception("No implemented behavior for option " + option);
        }
        Debug.Log(option + " VALUE NOW " + newValue);
        NotifyObservers();
    }
    public bool GetBooleanOption(BooleanOptionEnum option)
    {
        return option switch
        {
            BooleanOptionEnum.ACCESSIBILITY_ENABLED => accessibilityEnabled,
            BooleanOptionEnum.NARRATION_MUTED => musicMuted,
            BooleanOptionEnum.HIGH_CONTRAST_ON => highContrastOn,
            BooleanOptionEnum.LARGER_POWERUPS_ON => largerPowerupsOn,
            BooleanOptionEnum.LARGER_PROJECTILES_ON => largerProjectilesOn,
            BooleanOptionEnum.EDGE_GUARD_ON => edgeGuardOn,
            BooleanOptionEnum.AUTO_FIRE_ON => autoFireOn,
            BooleanOptionEnum.AUTO_JUMP_ON => autoJumpOn,
            BooleanOptionEnum.SPRITE_OUTLINES_ON => spriteOutlinesOn,
            BooleanOptionEnum.SMALL_HEALTH_RING => smallHealthRing,
            BooleanOptionEnum.AUDIO_NAVIGATION_ON => audioNavigationOn,
            BooleanOptionEnum.ENVIRONMENT_PROXIMITY_ON => environmentProximityOn,
            _ => throw new Exception("No implemented behavior for option " + option),
        };
    }


    public void Pause()
    {
        Time.timeScale = 0;
        SoundEffectHolder.instance.PauseAllNonNarration();
        IsPaused = true;
    }
    public void Unpause()
    {
        Time.timeScale = defaultGameplaySpeed;
        SoundEffectHolder.instance.UnpauseAllNonNarration();
        IsPaused = false;
    }

    public void SetGameplaySpeed(int val)
    {
        Debug.Log("SPEED " + val);
        gameplaySpeedVal = val;
        switch (val)
        {
            case 0:
                defaultGameplaySpeed = 1f;
                break;
            case 1:
                defaultGameplaySpeed = 0.8f;
                break;
            case 2:
                defaultGameplaySpeed = 0.6f;
                break;
        }
    }

    public int GetGameplaySpeedVal()
    {
        return gameplaySpeedVal;
    }

    public void SetDifficulty(int val)
    {
        Debug.Log("DIFFICULTY " + val);
        currentDifficulty = val switch
        {
            0 => standardDifficulty,
            1 => easyDifficulty,
            2 => hardDifficulty,
            3 => invulnerableDifficulty,
            _ => standardDifficulty,
        };
    }

    public int GetDifficulty()
    {
        return currentDifficulty.currentDifficulty switch
        {
            DifficultyEnum.STANDARD => 0,
            DifficultyEnum.EASY => 1,
            DifficultyEnum.HARD => 2,
            DifficultyEnum.INVULNERABLE => 3,
            _ => 0,
        };
    }

    public class DifficultyOptions
    {
        public DifficultyEnum currentDifficulty;
        public float enemySpeedMultiplier; //check
        public float enemyDamageMultiplier; //check
        public float enemyImmunityFrameMultiplier; //check

        public float playerImmunityFrameMultiplier; // check
        public float playerProjectileFrequencyMultiplier; // check
        public float playerProjectileEffectDurationMultiplier; // check

        public DifficultyOptions(DifficultyEnum difficultyEnum)
        {
            currentDifficulty = difficultyEnum;
            switch (difficultyEnum)
            {
                case DifficultyEnum.STANDARD:
                    InitStandard();
                    break;
                case DifficultyEnum.EASY:
                    InitEasy();
                    break;
                case DifficultyEnum.HARD:
                    InitHard();
                    break;
                case DifficultyEnum.INVULNERABLE:
                    InitInvulnerable();
                    break;
                default:
                    InitStandard();
                    break;
            }
        }

        private void InitStandard()
        {
            enemySpeedMultiplier = 1;
            enemyDamageMultiplier = 1;
            // gameplaySpeedMultiplier = 1;
            playerImmunityFrameMultiplier = 1;
            enemyImmunityFrameMultiplier = 1;
            playerProjectileFrequencyMultiplier = 1;
            playerProjectileEffectDurationMultiplier = 1;
        }

        private void InitEasy()
        {
            enemySpeedMultiplier = 0.5f;
            enemyDamageMultiplier = 0;
            // gameplaySpeedMultiplier = 1;
            playerImmunityFrameMultiplier = 2;
            enemyImmunityFrameMultiplier = 1;
            playerProjectileFrequencyMultiplier = 0.5f;
            playerProjectileEffectDurationMultiplier = 1;
        }

        private void InitInvulnerable()
        {
            enemySpeedMultiplier = 1;
            enemyDamageMultiplier = 0;
            // gameplaySpeedMultiplier = 1;
            playerImmunityFrameMultiplier = 1;
            enemyImmunityFrameMultiplier = 1;
            playerProjectileFrequencyMultiplier = 1;
            playerProjectileEffectDurationMultiplier = 1;
        }

        private void InitHard()
        {
            enemySpeedMultiplier = 1;
            enemyDamageMultiplier = 1;
            // gameplaySpeedMultiplier = 1;
            playerImmunityFrameMultiplier = 1;
            enemyImmunityFrameMultiplier = 1;
            playerProjectileFrequencyMultiplier = 1;
            playerProjectileEffectDurationMultiplier = 1;
        }
    }

}
