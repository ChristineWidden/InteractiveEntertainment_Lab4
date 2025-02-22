using UnityEngine;
using UnityEngine.Tilemaps;

public class DisappearOnLoadTilemap : MonoBehaviour
{
    TilemapRenderer tilemapRenderer;

    void OnEnable()
    {
        TryGetComponent<TilemapRenderer>(out tilemapRenderer);
        if (tilemapRenderer) tilemapRenderer.enabled = false;
    }
}
