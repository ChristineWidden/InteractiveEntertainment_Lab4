using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearOnLoad : MonoBehaviour
{

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    void OnEnable()
    {
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        if (spriteRenderer) spriteRenderer.enabled = false;
        

    }

}
