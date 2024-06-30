using UnityEngine;
using UnityEngine.UI;

public class NPCConversation : MonoBehaviour
{
    public MissionManager missionManager;
    public GameObject dialogueBox;
    public GameObject dialogueText;
    public string npcName;  // Set to "NPC1" or "NPC2" in the Inspector

    private bool isPlayerInRange = false;

    void Update()
    {
        if (isPlayerInRange)
        {
            if (npcName == "NPC1")
            {
                missionManager.TalkToNPC1();
            }
            else if (npcName == "NPC2")
            {
                missionManager.TalkToNPC2();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }
}
