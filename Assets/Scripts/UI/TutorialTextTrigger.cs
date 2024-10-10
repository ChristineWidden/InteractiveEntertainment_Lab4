using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;
using System;

public class TutorialTextTrigger : MonoBehaviour
{
    [SerializeField] private bool canTriggerAgain;
    private bool alreadyTriggered = false;

    [SerializeField, TextArea]
    private string serializedTextField;
    private TextMeshProUGUI textDisplay;

    private PlayerInput playerInput;

    private string _Movement_negative;
    private string _Movement_positive;
    private string _Jump;
    private string _Crouch;
    private string _ThrowRock;
    private string _ToggleThrowRock;
    private string _Pause;
    private string _Toggle_Movement_negative;
    private string _Toggle_Movement_positive;


    void Start()
    {
        textDisplay = TextDisplaySingleton.instance.GetComponent<TextMeshProUGUI>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && (canTriggerAgain || !alreadyTriggered))
        {

            textDisplay.text = ProcessString(serializedTextField);
            alreadyTriggered = true;
        }
    }

    private string ProcessString(string str)
    {
        string pattern = @"&\w+"; // Matches an '&' followed by one or more word characters

        MatchCollection matches = Regex.Matches(str, pattern);

        foreach (Match match in matches)
        {
            string[] x = match.Value[1..].Split('_');

            InputBinding binding0 = playerInput.actions[x[0]].bindings[0];

            string replaceval = "";

            Debug.Log(InputControlPath.ToHumanReadableString(binding0.effectivePath));

            if (InputControlPath.ToHumanReadableString(binding0.effectivePath) == " [1DAxis]")
            {
                Debug.Log("Got this far");
                if (x[1] == "negative")
                {
                    replaceval = InputControlPath.ToHumanReadableString(playerInput.actions[x[0]].bindings[1].effectivePath);
                }
                else if (x[1] == "positive")
                {
                    replaceval = InputControlPath.ToHumanReadableString(playerInput.actions[x[0]].bindings[2].effectivePath);
                }
                else
                {
                    replaceval = "ERROR";
                }
            }
            else
            {
                replaceval = InputControlPath.ToHumanReadableString(binding0.effectivePath);
            }
            str = str.Replace(match.Value, replaceval);

            Debug.Log(match.Value + " modified " + x[0]);
            Debug.Log(str);
        }
        return str;
    }
}
