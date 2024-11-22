using UnityEngine;
using UnityEngine.Tilemaps;

public class EdgeGuard : IOptionObserver
{

    TilemapRenderer tilemapRenderer;
    TilemapCollider2D tilemapCollider2D;

    private new void OnEnable()
    {
        base.OnEnable();
        tilemapRenderer = GetComponent<TilemapRenderer>();
        tilemapCollider2D = GetComponent<TilemapCollider2D>();
        OnOptionChanged();
    }

    public override void OnOptionChanged() {
        bool active = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.EDGE_GUARD_ON);

        tilemapRenderer.enabled = active;
        tilemapCollider2D.enabled = active;
    }
}
