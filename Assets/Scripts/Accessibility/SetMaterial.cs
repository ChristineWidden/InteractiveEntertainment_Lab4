using UnityEngine;

public class SetMaterial : IOptionObserver
{
    public Material standardRenderMaterial; // Assign the new material in the Unity Editor
    public Material highContrastMaterial;
    private Renderer objectRenderer;

    private new void OnEnable()
    {
        base.OnEnable();
    
        if (!TryGetComponent<Renderer>(out objectRenderer))
        {
            Debug.LogError("Renderer component not found on the GameObject.");
        }

        UpdateContrast();
    }
    public override void OnOptionChanged() {
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
