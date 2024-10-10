using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class CopyOverridenBindings : IActionUser
{
    private PlayerInput playerInput;
    [SerializeField] private InputActionAsset originalInputs;
    [SerializeField] private string actionMapName;

    void Start() {
        playerInput = GetComponent<PlayerInput>();
    }

    public override void OnActionsUpdated()
    {
        CopyOverridenBindingsFunc(
            playerInput.actions.FindActionMap(actionMapName),
            originalInputs.FindActionMap(actionMapName)
        );
    }

    private void CopyOverridenBindingsFunc(InputActionMap copyActionMap, InputActionMap originalActionMap)
    {
        for (int i = 0; i < copyActionMap.actions.Count; i++)
        {
            var copyAction = copyActionMap.actions[i];
            var originalAction = originalActionMap.actions[i];
            for (int j = 0; j < copyAction.bindings.Count; j++)
            {
                var originalBinding = originalAction.bindings[j];
                if (originalBinding.overridePath != null
                    && originalBinding.overridePath != string.Empty)
                {
                    copyAction.ChangeBinding(j).WithPath(originalBinding.overridePath);
                }
            }
        }
    }
}