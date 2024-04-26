using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHolder : MonoBehaviour
{
    public static GameObject instance;

    // Update is called once per frame
    void Awake()
    {
        instance = gameObject;
    }
}
