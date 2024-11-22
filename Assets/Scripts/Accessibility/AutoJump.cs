using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoJump : IOptionObserver
{

    TilemapCollider2D tilemapCollider;

    private new void OnEnable()
    {
        base.OnEnable();
        
        tilemapCollider = GetComponent<TilemapCollider2D>();
        OnOptionChanged();
    }

    public override void OnOptionChanged() {
        bool active = OptionsManager.Instance.GetBooleanOption(BooleanOptionEnum.AUTO_JUMP_ON);

        Debug.Log("AUTOJUMP SUCCESSFULLY CALLED. ACTIVE IS " + active);

        tilemapCollider.enabled = active;
    }
}
