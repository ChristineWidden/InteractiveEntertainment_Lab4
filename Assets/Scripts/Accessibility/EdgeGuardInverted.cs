using UnityEngine;
using UnityEngine.Tilemaps;

public class EdgeGuardInverted : IOptionObserver
{
    [SerializeField] private bool doRender;

    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2D;

    private new void OnEnable()
    {
        base.OnEnable();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        OnOptionChanged();
    }

    public override void OnOptionChanged() {
        bool active = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.EDGE_GUARD_ON);

        spriteRenderer.enabled = doRender & !active;
        boxCollider2D.enabled = !active;
    }
}
