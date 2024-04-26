using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedLife : MonoBehaviour
{

    [SerializeField] private float lifespan;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", lifespan);
    }

    // Update is called once per frame
    void Die()
    {
        Destroy(gameObject);
    }
}
