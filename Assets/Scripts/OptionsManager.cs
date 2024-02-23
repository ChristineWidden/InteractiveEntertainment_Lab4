using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum BooleanOptionEnum
{
    MUSIC_MUTED,
    HIGH_CONTRAST_ON,
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
        switch (option)
        {
            case BooleanOptionEnum.MUSIC_MUTED:
                return musicMuted;
            case BooleanOptionEnum.HIGH_CONTRAST_ON:
                return highContrastOn;
            default:
                throw new Exception("No implemented behavior for option " + option);
        }
    }

}
