using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearOnLoad : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    void OnEnable()
    {
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        if (spriteRenderer) spriteRenderer.enabled = false;
    }
}
