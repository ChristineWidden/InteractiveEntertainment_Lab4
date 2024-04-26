using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialTextTrigger : MonoBehaviour
{
    [SerializeField] private bool canTriggerAgain;
    private bool alreadyTriggered = false;
    
    [SerializeField, TextArea]
    private string serializedTextField;
    private TextMeshProUGUI textDisplay;


    // Start is called before the first frame update
    void Start()
    {
        textDisplay = TextDisplaySingleton.instance.GetComponent<TextMeshProUGUI>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && (canTriggerAgain || !alreadyTriggered))
        {
            textDisplay.text = serializedTextField;
            alreadyTriggered = true;
        }
    }
}
