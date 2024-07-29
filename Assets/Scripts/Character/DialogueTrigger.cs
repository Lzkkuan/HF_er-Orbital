using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Triggers dialogue interactions when the player enters the trigger zone.
/// </summary>
public class DialogueTrigger : MonoBehaviour
{
    /// <summary>
    /// The dialogue data to be triggered.
    /// </summary>
    public Dialogue dialogue;

    private bool dialogueStarted = false;

    /// <summary>
    /// Called when another collider enters the trigger zone.
    /// </summary>
    /// <param name="other">The collider that entered the trigger zone.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueStarted)
        {
            Debug.Log("Player entered trigger");
            TriggerDialogue();
        }
    }

    /// <summary>
    /// Called when another collider exits the trigger zone.
    /// </summary>
    /// <param name="other">The collider that exited the trigger zone.</param>
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueStarted = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IDialogueManager.instance.IsDialogueBoxActive())
        {
            IDialogueManager.instance.DisplayNextSentence();
        }
    }

    /// <summary>
    /// Starts the dialogue if it is not already active.
    /// </summary>
    public void TriggerDialogue()
    {
        if (!IDialogueManager.instance.IsDialogueBoxActive())
        {
            IDialogueManager.instance.StartDialogue(dialogue);
            dialogueStarted = true;
        }
    }
}
