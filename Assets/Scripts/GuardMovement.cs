using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardMovement : MonoBehaviour
{

    public float moveSpeed;
    private bool moveRight;
    public float moveLimit;

    public float currentHealth = 100;

    const int DAMAGE = 30;

    private int hurting;
    public int DAMAGE_BUFFER = 30;

    public static int guardCount = 2;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 2f;
        moveRight = true;
        hurting = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (hurting > 0) {
            hurting = hurting - 1;
        }

        if (transform.position.x > moveLimit) {
            moveRight = false;
        } else if (transform.position.x < moveLimit * -1) {
            moveRight = true;
        }

        transform.position = new Vector2(transform.position.x + (moveRight ? 1 : -1) * moveSpeed * Time.deltaTime, transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hurting == 0 && other.gameObject.CompareTag("Rock")) {
            Debug.Log("GUARD HURT!!! Guard count is " + guardCount.ToString());
            hurting = DAMAGE_BUFFER;
            currentHealth -= DAMAGE;
            if (currentHealth < 1){
                
                guardCount -= 1;

                Debug.Log("GUARD IS DEAD!!!!!");
                

                if (guardCount == 0) {
                    SceneManager.LoadScene("WinScene");
                }

                Destroy(gameObject);
            }
        }
    }
}
