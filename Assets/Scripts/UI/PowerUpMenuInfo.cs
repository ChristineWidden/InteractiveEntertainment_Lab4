using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpMenuInfo : MonoBehaviour
{

    TextMeshProUGUI text;
    Image image;

    [SerializeField] private PlayerProjectile rockProjectile;
    [SerializeField] private PlayerProjectile appleProjectile;
    [SerializeField] private PlayerProjectile pieProjectile;
    [SerializeField] private PlayerProjectile tomatoProjectile;
    [SerializeField] private PlayerProjectile roastProjectile;

    void Awake()
    {
        // Debug.Log("Awake!");
        TryGetComponent<TextMeshProUGUI>(out text);
        TryGetComponent<Image>(out image);
    }

    void OnEnable()
    {
        // Debug.Log("Enabled!");
        if (text)
        {
            // Debug.Log("UpdatePowerUpMenuText!");
            UpdatePowerUpMenuText(CrossSceneCommunicator.Instance.powerUp);
        }
        if (image)
        {
            // Debug.Log("UpdatePowerUpMenuSprite!");
            UpdatePowerUpMenuSprite(CrossSceneCommunicator.Instance.powerUpSprite);
        }
    }

    void UpdatePowerUpMenuText(PowerUpEnum powerUp)
    {
        text.text = powerUp switch
        {
            // PowerUpEnum.Rock => "The standard projectile. Briefly stuns enemies, letting you walk past them unharmed.",
            PowerUpEnum.Rock => rockProjectile.GetDescription(),
            PowerUpEnum.Apple => appleProjectile.GetDescription(),
            PowerUpEnum.Pie => pieProjectile.GetDescription(),
            PowerUpEnum.Tomato => tomatoProjectile.GetDescription(),
            PowerUpEnum.Roast => roastProjectile.GetDescription(),
            _ => "Should be a power up here why isn't there",
        };
    }

    void UpdatePowerUpMenuSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}