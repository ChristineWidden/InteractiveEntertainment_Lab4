using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Text.RegularExpressions;
using UnityEngine.Events;
using System;

public class TutorialTextTrigger : IOptionObserver
{
    private bool debugMessages = false;

    [SerializeField] private bool canTriggerAgain;
    private bool alreadyTriggered = false;

    [SerializeField, TextArea]
    private string serializedTextField;
    private TextMeshProUGUI textDisplay;
    public UnityEvent onStartCollision;



    private PlayerInput playerInput;

    void Start()
    {
        textDisplay = TextDisplaySingleton.instance.GetComponent<TextMeshProUGUI>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && (canTriggerAgain || !alreadyTriggered))
        {
            onStartCollision.Invoke();
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

            if (debugMessages) Debug.Log(InputControlPath.ToHumanReadableString(binding0.effectivePath));

            if (InputControlPath.ToHumanReadableString(binding0.effectivePath) == " [1DAxis]")
            {
                if (debugMessages) Debug.Log("Got this far");
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

            if (debugMessages) Debug.Log(match.Value + " modified " + x[0]);
            if (debugMessages) Debug.Log(str);
        }
        return str;
    }

    public override void OnOptionChanged()
    {
        textDisplay.text = ProcessString(serializedTextField);
    }
}
