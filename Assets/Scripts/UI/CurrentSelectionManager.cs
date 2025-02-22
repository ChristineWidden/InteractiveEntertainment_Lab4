using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CurrentSelectionManager : MonoBehaviour
{

    private bool doDebugPrints = false;

    public static CurrentSelectionManager Instance = null;
    GameObject selectedObj = null;
    [SerializeField] public UnityEvent<GameObject> selectionChangedEvent;    

    private void OnEnable()
    {
        Debug.Log("Current Selection Manager enabled");
        // Check if the instance already exists
        if (Instance != null && Instance != this)
        {
            Debug.Log("Current Selection Manager already exists, destroying copy", gameObject);
            Destroy(gameObject); // Destroy this instance if another already exists
            return;
        }

        Instance = this; // Assign this instance to the static property
        // DontDestroyOnLoad(gameObject); // Make the instance persist across scenes
    }

    void Update()
    {
        GameObject newSelectedObj = EventSystem.current.currentSelectedGameObject;
        // CheckNotifyUpdate(newSelectedObj);
    }

    public void UpdateSelection(GameObject newSelectedObj) {        
        CheckNotifyUpdate(newSelectedObj);
    }

    private void CheckNotifyUpdate(GameObject newSelectedObj) {
        if (newSelectedObj == null) {
            if (doDebugPrints) Debug.Log("New selected object was null.");
            return;
        }
        if (newSelectedObj == selectedObj) {
            if (doDebugPrints) Debug.Log("New selected object was the same as the previous.");
            return;
        }

        selectedObj = newSelectedObj;
        selectionChangedEvent.Invoke(selectedObj);
    }
}
