using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PowerUpMenuInfo : MonoBehaviour
{

    TextMeshProUGUI text;
    Image image;

    void Awake()
    {
        Debug.Log("Awake!");
        TryGetComponent<TextMeshProUGUI>(out text);
        TryGetComponent<Image>(out image);
    }

    void OnEnable()
    {
        Debug.Log("Enabled!");
        if (text)
        {
            Debug.Log("UpdatePowerUpMenuText!");
            UpdatePowerUpMenuText(CrossSceneCommunicator.Instance.powerUp);
        }
        if (image)
        {
            Debug.Log("UpdatePowerUpMenuSprite!");
            UpdatePowerUpMenuSprite(CrossSceneCommunicator.Instance.powerUpSprite);
        }
    }

    void UpdatePowerUpMenuText(PowerUpEnum powerUp)
    {
        text.text = powerUp switch
        {
            PowerUpEnum.Rock => "Rock stuff",
            PowerUpEnum.Apple => "Apple stuff",
            PowerUpEnum.Pie => "Pie stuff",
            PowerUpEnum.Tomato => "Tomato stuff",
            PowerUpEnum.Roast => "Roast stuff",
            _ => "Should be a power up here why isn't there",
        };
    }

    void UpdatePowerUpMenuSprite(Sprite sprite)
    {
        image.sprite = sprite;
    }
}