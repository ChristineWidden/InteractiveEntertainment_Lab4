using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextDisplaySingleton : MonoBehaviour
{
    // SINGLETON PATTERN
    public static GameObject instance;
    void Awake()
    {
        instance = gameObject;
    }
}