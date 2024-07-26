using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialogueBox;
    public Text dialogueText, nameText;

    [TextArea(1, 3)]
    public string[] npcLines;
    public string[] playerLines;
    public string npcName;
    public string playerName;
    private int currentLine;
    private bool isPlayerTurn; // Flag to check if it's the player's turn to speak
    private bool isConversation; // Flag to indicate if it's a conversation

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogueBox.SetActive(false);
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isConversation)
                {
                    if (currentLine < npcLines.Length + playerLines.Length - 1)
                    {
                        currentLine += 1;
                        UpdateDialogue();
                    }
                    else if (currentLine == npcLines.Length + playerLines.Length - 1)
                    {
                        dialogueBox.SetActive(false);
                    }
                }
                else
                {
                    if (currentLine < npcLines.Length - 1)
                    {
                        currentLine += 1;
                        dialogueText.text = npcLines[currentLine];
                    }
                    else if (currentLine == npcLines.Length - 1)
                    {
                        dialogueBox.SetActive(false);
                    }
                }
            }
        }
    }

    public void ShowDialogue(string[] _npcLines, string _npcName)
    {
        npcLines = _npcLines;
        npcName = _npcName;
        currentLine = 0;
        isConversation = false; // It's a solo NPC dialogue
        dialogueText.text = npcLines[currentLine];
        nameText.text = npcName;
        dialogueBox.SetActive(true);
    }

    public void ShowDialogue(string[] _npcLines, string[] _playerLines, string _npcName, string _playerName, bool _isConversation)
    {
        npcLines = _npcLines;
        playerLines = _playerLines;
        npcName = _npcName;
        playerName = _playerName;
        currentLine = 0;
        isConversation = _isConversation; // It's a conversation
        isPlayerTurn = false; // NPC starts first
        UpdateDialogue();
        dialogueBox.SetActive(true);
    }

    private void UpdateDialogue()
    {
        if (isPlayerTurn)
        {
            dialogueText.text = playerLines[currentLine / 2];
            nameText.text = playerName;
        }
        else
        {
            dialogueText.text = npcLines[currentLine / 2];
            nameText.text = npcName;
        }
        isPlayerTurn = !isPlayerTurn; // Toggle the turn
    }
}
