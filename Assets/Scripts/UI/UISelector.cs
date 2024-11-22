using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UISelector : MonoBehaviour
{

    public GameObject firstSelected;
    [SerializeField] private EventSystem eventSystem;

    public void OnEnable()
    {
        eventSystem.SetSelectedGameObject(firstSelected);
    }

}
