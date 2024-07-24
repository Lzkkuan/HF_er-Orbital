
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;
    public GameObject dialogueBox;
    public Text dialogueText, nameText;

    [TextArea(1, 3)]
    public string[] dialogueLines;
    [SerializeField] private int currentLine;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        dialogueText.text = dialogueLines[currentLine];
    }

    private void Update()
    {
        if (dialogueBox.activeInHierarchy)
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (currentLine < dialogueLines.Length - 1)
                {
                    currentLine += 1;
                    dialogueText.text = dialogueLines[currentLine];
                }
                else if (currentLine == dialogueLines.Length - 1) { dialogueBox.SetActive(false); }
            }
        }

    }

    public void showDialogue(string[] _newLines)
    {
        dialogueLines = _newLines;
        currentLine = 0;
        dialogueText.text = dialogueLines[currentLine];
        dialogueBox.SetActive(true);
    }
        

}


/* 
 * else
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                if (currentLine < dialogueLines.Length - 1)
                {
                    currentLine += 1;
                    dialogueText.text = dialogueLines[currentLine];
                }
                else if (currentLine == dialogueLines.Length - 1) {
                    dialogueBox.SetActive(false);
                }
            }
        }
    }
*/
