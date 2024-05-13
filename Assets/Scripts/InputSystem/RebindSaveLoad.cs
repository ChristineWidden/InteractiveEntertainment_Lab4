using UnityEngine;
using UnityEngine.InputSystem;

public class RebindSaveLoad : IOptionObserver
{
    public InputActionAsset actions;

    public new void OnEnable()
    {
        base.OnEnable();

        Debug.Log("Loading controls");
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public override void OnOptionChanged()
    {
        var rebinds = PlayerPrefs.GetString("rebinds");
        if (!string.IsNullOrEmpty(rebinds))
            actions.LoadBindingOverridesFromJson(rebinds);
    }

    public new void OnDisable()
    {
        base.OnDisable();

        Debug.Log("Saving controls");
        var rebinds = actions.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString("rebinds", rebinds);
    }

    
}
