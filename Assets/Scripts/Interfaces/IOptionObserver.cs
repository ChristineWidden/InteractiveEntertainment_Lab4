using UnityEngine;

public abstract class IOptionObserver: MonoBehaviour
{
    protected void OnEnable()
    {
        OptionsManager.Instance.RegisterObserver(this);
    }
    protected void OnDisable()
    {
        OptionsManager.Instance.UnregisterObserver(this);
    }

    public abstract void OnOptionChanged();
}