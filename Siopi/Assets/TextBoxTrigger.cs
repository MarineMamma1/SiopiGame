using UnityEngine;
using UnityEngine.UI;

public class TextBoxTrigger : MonoBehaviour
{
    public string message = "Hello, player!";
    public float displayTime = 3f; // Time in seconds to display the message
    public bool displayOnlyOnce = true; // Display message only once

    private Text textBox;
    private bool hasDisplayed;

    void Start()
    {
        // Find the UI Text component
        textBox = FindObjectOfType<Text>();
        if (textBox == null)
        {
            Debug.LogError("No UI Text component found in the scene.");
            return;
        }
        // Hide the text initially
        textBox.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasDisplayed)
        {
            // Display the message
            textBox.text = message;
            textBox.enabled = true;
            hasDisplayed = true;

            // Hide the message after displayTime seconds
            Invoke("HideTextBox", displayTime);
        }
    }

    void HideTextBox()
    {
        // Hide the text
        textBox.enabled = false;
    }

    void OnTriggerExit(Collider other)
    {
        // Hide the text when the player exits the trigger zone
        if (other.CompareTag("Player"))
        {
            HideTextBox();
        }
    }
}
