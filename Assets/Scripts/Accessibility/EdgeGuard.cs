using UnityEngine;
using UnityEngine.Tilemaps;

public class EdgeGuard : IOptionObserver
{
    [SerializeField] private bool invert = false;

    TilemapRenderer tilemapRenderer;
    SpriteRenderer spriteRenderer;
    TilemapCollider2D tilemapCollider2D;
    BoxCollider2D boxCollider2D;

    private new void OnEnable()
    {
        base.OnEnable();
        TryGetComponent<TilemapRenderer>(out tilemapRenderer);
        TryGetComponent<SpriteRenderer>(out spriteRenderer);
        TryGetComponent<TilemapCollider2D>(out tilemapCollider2D);
        TryGetComponent<BoxCollider2D>(out boxCollider2D);
        OnOptionChanged();
    }

    public override void OnOptionChanged() {
        bool active = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.EDGE_GUARD_ON);
        if (invert) {
            active = !active;
        }

        Debug.Log("Switching edge guard to " + active);
        if (tilemapRenderer != null) tilemapRenderer.enabled = active;
        if (spriteRenderer != null) spriteRenderer.enabled = active;
        if (tilemapCollider2D != null) tilemapCollider2D.enabled = active;
        if (boxCollider2D != null) boxCollider2D.enabled = active;
    }
}
