using UnityEngine;
using UnityEngine.InputSystem;

public class RebindMenuManager : MonoBehaviour
{
    public InputActionReference MoveRef, JumpRef, CrouchRef, ThrowRef;
    
    void OnEnable()
    {
        MoveRef.action.Disable();
        JumpRef.action.Disable();
        CrouchRef.action.Disable();
        ThrowRef.action.Disable();
    }

    void OnDisable()
    {
        MoveRef.action.Enable();
        JumpRef.action.Enable();
        CrouchRef.action.Enable();
        ThrowRef.action.Enable();
    }
}
