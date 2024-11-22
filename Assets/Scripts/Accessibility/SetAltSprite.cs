using UnityEngine;

public class SetAltSprite : IOptionObserver
{
    public Sprite standardSprite; // Assign the new material in the Unity Editor
    public Sprite alternateSprite;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private BooleanOptionEnum associatedOption;

    private new void OnEnable()
    {
        base.OnEnable();
    
        if (!TryGetComponent<SpriteRenderer>(out spriteRenderer))
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
        bool highContrast = OptionsManager.Instance.GetBooleanOption(associatedOption);
        if (highContrast) {
            spriteRenderer.sprite = alternateSprite;
            //Debug.Log("Contrast set to high");
        } else {
            spriteRenderer.sprite = standardSprite;
            //Debug.Log("Contrast set to low");
        }
    }
}
