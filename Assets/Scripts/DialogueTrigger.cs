using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private bool dialogueStarted = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !dialogueStarted)
        {
            Debug.Log("Player entered trigger");
            TriggerDialogue();
        }
    }

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

    public void TriggerDialogue()
    {
        if (!IDialogueManager.instance.IsDialogueBoxActive())
        {
            IDialogueManager.instance.StartDialogue(dialogue);
            dialogueStarted = true;
        }
    }
}
