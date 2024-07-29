using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the dialogue system, handling the display and progression of dialogue.
/// </summary>
public class IDialogueManager : MonoBehaviour
{
    public static IDialogueManager instance; // Singleton instance of the dialogue manager

    public Text nameText; // UI Text component for displaying the name of the speaker
    public Text dialogueText; // UI Text component for displaying the dialogue text
    public GameObject dialogueBox; // Reference to the dialogue box UI element
    private Queue<string> sentences; // Queue to store the sentences for the dialogue
    private bool isDialogueActive = false; // Flag to check if a dialogue is currently active

    private void Awake()
    {
        // Ensure there is only one instance of IDialogueManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    private void Start()
    {
        sentences = new Queue<string>(); // Initialize the sentence queue
        dialogueBox.SetActive(false); // Hide the dialogue box at the start
    }

    /// <summary>
    /// Starts a new dialogue with the given Dialogue data.
    /// </summary>
    /// <param name="dialogue">The Dialogue object containing the dialogue data.</param>
    public void StartDialogue(Dialogue dialogue)
    {
        if (isDialogueActive)
        {
            Debug.LogWarning("Dialogue is already active. Cannot start a new one."); // Log a warning if dialogue is already active
            return;
        }

        Debug.Log("Starting dialogue with " + dialogue.name); // Log the start of the dialogue
        nameText.text = dialogue.name; // Set the speaker's name
        sentences.Clear(); // Clear the previous sentences
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // Enqueue each sentence
        }

        dialogueBox.SetActive(true); // Show the dialogue box
        isDialogueActive = true; // Set the flag to true
        DisplayNextSentence(); // Display the first sentence
    }

    /// <summary>
    /// Displays the next sentence in the dialogue.
    /// </summary>
    public void DisplayNextSentence()
    {
        StartCoroutine(DisplayNextSentenceCoroutine()); // Start the coroutine to display the next sentence
    }

    /// <summary>
    /// Coroutine to handle the display of the next sentence with a delay.
    /// </summary>
    /// <returns>IEnumerator for coroutine</returns>
    private IEnumerator DisplayNextSentenceCoroutine()
    {
        if (sentences.Count == 0)
        {
            EndDialogue(); // End the dialogue if no sentences are left
            yield break;
        }
        string sentence = sentences.Dequeue(); // Dequeue the next sentence
        Debug.Log("Dequeued sentence: " + sentence); // Log the dequeued sentence
        dialogueText.text = sentence; // Display the sentence in the UI
        Debug.Log("Displaying sentence in UI: " + sentence); // Log the displayed sentence

        // Ensure the UI has enough time to update
        yield return null;
        yield return new WaitForSeconds(0.4f); // Wait before displaying the next sentence (adjustable)
    }

    /// <summary>
    /// Ends the current dialogue and hides the dialogue box.
    /// </summary>
    private void EndDialogue()
    {
        Debug.Log("End of conversation"); // Log the end of the conversation
        dialogueBox.SetActive(false); // Hide the dialogue box
        isDialogueActive = false; // Reset the flag
    }

    /// <summary>
    /// Checks if the dialogue box is currently active.
    /// </summary>
    /// <returns>True if the dialogue box is active; otherwise, false.</returns>
    public bool IsDialogueBoxActive()
    {
        return dialogueBox.activeSelf; // Return the active state of the dialogue box
    }
}
