using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArrows : MonoBehaviour
{

    [SerializeField]
    private int arrowAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 arrowMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0f, 2f);
    }

    private void Fire() {
        float angleStep = (endAngle - startAngle) / arrowAmount;
        float angle = startAngle;

        

        for (int i = 0; i < arrowAmount + 1; i++) {

            float arrowDirectionX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float arrowDirectionY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 arrowMoveVector = new Vector3(arrowDirectionX, arrowDirectionY, 0f);
            Vector2 arrowDirection = (arrowMoveVector - transform.position).normalized;

            GameObject arrow = ArrowPool.arrowPoolInstance.GetArrow();
            arrow.transform.position = transform.position;
            arrow.transform.rotation = transform.rotation;
            arrow.SetActive(true);
            arrow.GetComponent<ArrowScript>().SetMoveDirection(arrowDirection);

            angle += angleStep;
        }

        
    }
}
