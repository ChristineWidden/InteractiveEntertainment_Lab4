using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class RebindSaveLoad : IOptionObserver
{
    public InputActionAsset actions;
    private List<IActionUser> actionUsers = new List<IActionUser>();

    // Singleton instance
    private static RebindSaveLoad instance;

    public static RebindSaveLoad Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("RebindSaveLoad").AddComponent<RebindSaveLoad>();
                DontDestroyOnLoad(instance.gameObject);
            }
            return instance;
        }
    }

    // Register an observer
    public void RegisterObserver(IActionUser observer)
    {
        if (!actionUsers.Contains(observer))
            actionUsers.Add(observer);
    }

    // Unregister an observer
    public void UnregisterObserver(IActionUser observer)
    {
        actionUsers.Remove(observer);
    }


    public new void OnEnable()
    {
        base.OnEnable();

        Debug.Log("RebindSaveLoad enabled", gameObject);
        // var rebinds = PlayerPrefs.GetString("rebinds");
        // if (!string.IsNullOrEmpty(rebinds))
        //     actions.LoadBindingOverridesFromJson(rebinds);
    }

    public void Start() {
        Debug.Log("RebindSaveLoad started, Loading controls", gameObject);
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
        foreach (var observer in actionUsers)
        {
            observer.OnActionsUpdated();
        }
    }

    public override void OnOptionChanged()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds)) {
            Debug.Log("Loading changed controls", gameObject);
            actions.LoadBindingOverridesFromJson(rebinds);
            foreach (var observer in actionUsers)
            {
                observer.OnActionsUpdated();
            }
        }
    }

    public new void OnDisable()
    {
        base.OnDisable();

        Debug.Log("Disabling RebindSaveLoad and Saving controls");
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
        Debug.Log("Controls saved successfully.");
    }

    
}
