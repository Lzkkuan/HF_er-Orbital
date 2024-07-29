using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] npcLines; ///< The lines of dialogue spoken by the NPC.
    public string[] playerLines; ///< The lines of dialogue spoken by the player. Add this line for player's dialogue.
    public string npcName; ///< The name of the NPC.
    public string playerName; ///< The name of the player. Add this line for player's name.
    public bool isConversation; ///< Determines if the interaction is a conversation. Make this public to set from the inspector.
    [SerializeField] private bool isPlayerInRange; ///< Tracks whether the player is within range for interaction.

    /// <summary>
    /// Sets the flag to true when the player enters the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object entering the trigger area.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    /// <summary>
    /// Sets the flag to false when the player exits the trigger area.
    /// </summary>
    /// <param name="other">The collider of the object exiting the trigger area.</param>
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    /// <summary>
    /// Checks for player input to show dialogue if the player is in range and the dialogue box is not currently active.
    /// </summary>
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyUp(KeyCode.F) && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            if (isConversation)
            {
                DialogueManager.instance.ShowDialogue(npcLines, playerLines, npcName, playerName, isConversation); ///< Updates the dialogue manager to show conversation.
            }
            else
            {
                DialogueManager.instance.ShowDialogue(npcLines, npcName); ///< Updates the dialogue manager to show a single line dialogue.
            }
        }
    }
}
