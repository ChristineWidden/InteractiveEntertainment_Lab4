using UnityEngine;

public class SetMaterial : MonoBehaviour, IOptionObserver
{
    public Material standardRenderMaterial; // Assign the new material in the Unity Editor
    public Material highContrastMaterial;
    private Renderer objectRenderer;

    private void OnEnable()
    {
        OptionsManager.Instance.RegisterObserver(this);

        // Get the Renderer component of the GameObject
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer == null)
        {
            Debug.LogError("Renderer component not found on the GameObject.");
        }

        UpdateContrast();
    }

    private void OnDisable()
    {
        OptionsManager.Instance.UnregisterObserver(this);
    }

    //TODO use events instead
    public void OnOptionChanged() {
        UpdateContrast();
    }

    void UpdateContrast() {
        //Debug.Log("Updating Contrast");
        bool highContrast = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.HIGH_CONTRAST_ON);
        if (highContrast) {
            objectRenderer.material = highContrastMaterial;
            //Debug.Log("Contrast set to high");
        } else {
            objectRenderer.material = standardRenderMaterial;
            //Debug.Log("Contrast set to low");
        }
    }
}
