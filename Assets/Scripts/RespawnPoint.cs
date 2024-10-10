using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    // SINGLETON PATTERN
    public static GameObject instance;
    void Awake()
    {
        instance = gameObject;
    }
}