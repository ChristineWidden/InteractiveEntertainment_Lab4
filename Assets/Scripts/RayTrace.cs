using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class RayTrace : MonoBehaviour
{
    [SerializeField] private List<string> targets;

    public int numCollisions {get; private set;}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targets.Contains(other.gameObject.tag))
        {
            numCollisions++;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (targets.Contains(other.gameObject.tag))
        {
            numCollisions--;
        }
    }

}
