using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the dialogue system, handling both solo NPC dialogues and conversations between NPC and player.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance; // Singleton instance of DialogueManager
    public GameObject dialogueBox; // Reference to the dialogue box UI element
    public Text dialogueText, nameText; // References to UI Text components for dialogue and names

    [TextArea(1, 3)]
    public string[] npcLines; // Lines spoken by the NPC
    public string[] playerLines; // Lines spoken by the player
    public string npcName; // Name of the NPC
    public string playerName; // Name of the player
    private int currentLine; // Index of the current line in the dialogue
    private bool isPlayerTurn; // Flag to check if it's the player's turn to speak
    private bool isConversation; // Flag to indicate if it's a conversation

    private void Awake()
    {
        instance = this; // Initialize the singleton instance
    }

    private void Start()
    {
        dialogueBox.SetActive(false); // Hide the dialogue box initially
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (isConversation)
                {
                    // Handle conversation dialogue
                    if (currentLine < npcLines.Length + playerLines.Length - 1)
                    {
                        currentLine += 1;
                        UpdateDialogue();
                    }
                    else if (currentLine == npcLines.Length + playerLines.Length - 1)
                    {
                        dialogueBox.SetActive(false); // Close the dialogue box
                    }
                }
                else
                {
                    // Handle solo NPC dialogue
                    if (currentLine < npcLines.Length - 1)
                    {
                        currentLine += 1;
                        dialogueText.text = npcLines[currentLine];
                    }
                    else if (currentLine == npcLines.Length - 1)
                    {
                        dialogueBox.SetActive(false); // Close the dialogue box
                    }
                }
            }
        }
    }

    /// <summary>
    /// Displays a solo NPC dialogue.
    /// </summary>
    /// <param name="_npcLines">Lines spoken by the NPC</param>
    /// <param name="_npcName">Name of the NPC</param>
    public void ShowDialogue(string[] _npcLines, string _npcName)
    {
        npcLines = _npcLines;
        npcName = _npcName;
        currentLine = 0;
        isConversation = false; // It's a solo NPC dialogue
        dialogueText.text = npcLines[currentLine];
        nameText.text = npcName;
        dialogueBox.SetActive(true); // Show the dialogue box
    }

    /// <summary>
    /// Displays a conversation between NPC and player.
    /// </summary>
    /// <param name="_npcLines">Lines spoken by the NPC</param>
    /// <param name="_playerLines">Lines spoken by the player</param>
    /// <param name="_npcName">Name of the NPC</param>
    /// <param name="_playerName">Name of the player</param>
    /// <param name="_isConversation">Flag to indicate if it's a conversation</param>
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
        dialogueBox.SetActive(true); // Show the dialogue box
    }

    /// <summary>
    /// Updates the dialogue text and name based on whose turn it is.
    /// </summary>
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
