using UnityEngine;

public abstract class IActionUser: MonoBehaviour
{
    // protected void OnEnable()
    // {
    //     RebindSaveLoad.Instance.RegisterObserver(this);
    // }
    // protected void OnDisable()
    // {
    //     RebindSaveLoad.Instance.UnregisterObserver(this);
    // }

    public abstract void OnActionsUpdated();
}