using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class UIHoverAndSelectTracker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{

    private bool doDebugPrints = true;
    public UnityEvent onSelect;

    public void OnPointerEnter(PointerEventData eventData)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        onSelect.Invoke();
        if (doDebugPrints) Debug.Log("Hovered over: " + gameObject.name);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (doDebugPrints) Debug.Log("Stopped hovering over: " + gameObject.name);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if (doDebugPrints) Debug.Log("Selecting: " + gameObject.name);
        onSelect.Invoke();
        CurrentSelectionManager.Instance.UpdateSelection(gameObject);
        if (doDebugPrints) Debug.Log("Selected: " + gameObject.name);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        if (doDebugPrints) Debug.Log("Deselected: " + gameObject.name);
    }
}
