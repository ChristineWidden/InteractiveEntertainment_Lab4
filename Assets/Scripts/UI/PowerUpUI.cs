using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUI : MonoBehaviour
{
    [SerializeField] private Sprite rockSprite;
    [SerializeField] private Sprite pieSprite;
    [SerializeField] private Sprite appleSprite;
    [SerializeField] private Sprite tomatoSprite;
    [SerializeField] private Sprite roastSprite;
    SpriteRenderer spriteRenderer;
    PowerUpEnum currentPowerUp;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetPowerUp(PowerUpEnum.Rock);

        CrossSceneCommunicator.Instance.powerUpUI = this;
    }

    public void SetPowerUp(PowerUpEnum powerUp)
    {
        currentPowerUp = powerUp;
        // Check if index is within bounds of the sprites array
        spriteRenderer.sprite = powerUp switch
        {
            PowerUpEnum.Rock => rockSprite,
            PowerUpEnum.Apple => appleSprite,
            PowerUpEnum.Pie => pieSprite,
            PowerUpEnum.Tomato => tomatoSprite,
            PowerUpEnum.Roast => roastSprite,
            _ => rockSprite,
        };
    }

    public Sprite GetSprite() {
        return spriteRenderer.sprite;
    }

    public PowerUpEnum GetPowerUp() {
        return currentPowerUp;
    }  

}
