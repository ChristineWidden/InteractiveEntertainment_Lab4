using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectHolder : MonoBehaviour
{
    public static GameObject instance;
    public static SoundEffectHolder soundEffectInstance;

    public AudioSource COLLECT_POWER_UP;
    public AudioSource GUARD_HURT;

    // Update is called once per frame
    void Awake()
    {
        instance = gameObject;
        soundEffectInstance = GetComponent<SoundEffectHolder>();
    }
}
