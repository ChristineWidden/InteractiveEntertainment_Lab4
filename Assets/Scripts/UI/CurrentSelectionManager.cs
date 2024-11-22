using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CurrentSelectionManager : MonoBehaviour
{

    public static CurrentSelectionManager Instance = null;
    GameObject selectedObj = null;
    [SerializeField] public UnityEvent<GameObject> selectionChangedEvent;    

    private void OnEnable()
    {
        // Check if the instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy this instance if another already exists
            return;
        }

        Instance = this; // Assign this instance to the static property
        // DontDestroyOnLoad(gameObject); // Make the instance persist across scenes
    }

    void Update()
    {
        GameObject newSelectedObj = EventSystem.current.currentSelectedGameObject;
        CheckNotifyUpdate(newSelectedObj);
    }

    public void UpdateSelection(GameObject newSelectedObj) {        
        CheckNotifyUpdate(newSelectedObj);
    }

    private void CheckNotifyUpdate(GameObject newSelectedObj) {
        if (newSelectedObj == null || newSelectedObj == selectedObj) return;

        selectedObj = newSelectedObj;
        selectionChangedEvent.Invoke(selectedObj);
    }
}
