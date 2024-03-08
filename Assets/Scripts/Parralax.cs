using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parralax : MonoBehaviour
{
    // TODO disable this for accessibility

    [SerializeField] private GameObject followObject;
    [SerializeField] private Vector2 originalPosition;
    [SerializeField] private float parralaxAmount;
    // Start is called before the first frame update

    void OnEnable() {
        originalPosition = new(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        float newX = originalPosition.x + parralaxAmount * followObject.transform.position.x;
        float newY = originalPosition.y + parralaxAmount * followObject.transform.position.y;
        transform.position = new(newX, transform.position.y, transform.position.z);
    }
}
