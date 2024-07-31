using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultipleTalkable : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] initialNpcLines; ///< Initial lines of dialogue spoken by the NPC.
    public string[] initialPlayerLines; ///< Initial lines of dialogue spoken by the player.
    public string[] missionNpcLines; ///< Lines spoken by the NPC when urging the player to complete the mission.
    public string[] missionPlayerLines; ///< Lines spoken by the player when being urged to complete the mission.
    public string[] postMissionNpcLines; ///< Lines spoken by the NPC after the mission is completed.
    public string[] postMissionPlayerLines; ///< Lines spoken by the player after the mission is completed.
    public string[] endMissionNPCLines; /// < Lines spoken after the final mission is completed;
    public string[] endMissionPlayerLines; /// < Lines spoken by player after final mission completed;
    public string npcName; ///< The name of the NPC.
    public string playerName; ///< The name of the player.
    public bool isConversation; ///< Determines if the interaction is a conversation.
    [SerializeField] private bool isPlayerInRange; ///< Tracks whether the player is within range for interaction.
    public bool isMissionOneCompleted; ///< Flag to track if the mission is completed.
    public bool isMissionTwoCompleted; /// < Flag to track is mission two is completed
    public bool initialLineSpoken = false;

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
            if (!initialLineSpoken)
            {
                if (isConversation) { DialogueManager.instance.ShowDialogue(initialNpcLines, initialPlayerLines, npcName, playerName, isConversation); }
                else { DialogueManager.instance.ShowDialogue(initialNpcLines, npcName); }
                initialLineSpoken = true;
            }
            else if (!isMissionOneCompleted)
            {
                if (isConversation) { DialogueManager.instance.ShowDialogue(missionNpcLines, missionPlayerLines, npcName, playerName, isConversation); }
                else { DialogueManager.instance.ShowDialogue(missionNpcLines, npcName); }
            }
            else if (!isMissionTwoCompleted)
            {
                if (isConversation) { DialogueManager.instance.ShowDialogue(postMissionNpcLines, postMissionPlayerLines, npcName, playerName, isConversation); }
                else { DialogueManager.instance.ShowDialogue(postMissionNpcLines, npcName);  }
            }
            else
            {
                if (isConversation) { DialogueManager.instance.ShowDialogue(endMissionNPCLines,endMissionPlayerLines,npcName,playerName,isConversation); }
                else { DialogueManager.instance.ShowDialogue(endMissionNPCLines, npcName); }
            }
        }
    }
}
