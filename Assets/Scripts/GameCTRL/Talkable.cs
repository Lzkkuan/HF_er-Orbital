using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] npcLines;
    public string[] playerLines; // Add this line for player's dialogue
    public string npcName;
    public string playerName; // Add this line for player's name
    public bool isConversation; // Make this public to set from the inspector
    [SerializeField] private bool isPlayerInRange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyUp(KeyCode.F) && !DialogueManager.instance.dialogueBox.activeInHierarchy)
        {
            if (isConversation)
            {
                DialogueManager.instance.ShowDialogue(npcLines, playerLines, npcName, playerName, isConversation); // Update this line
            }
            else
            {
                DialogueManager.instance.ShowDialogue(npcLines, npcName); // Update this line
            }
        }
    }
}
