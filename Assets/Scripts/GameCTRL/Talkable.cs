using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkable : MonoBehaviour
{
    [TextArea(1, 3)]
    public string[] lines;
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
        if (isPlayerInRange && Input.GetKeyUp(KeyCode.Space) && DialogueManager.instance.dialogueBox.activeInHierarchy == false)
        {
            DialogueManager.instance.showDialogue(lines);
        }
    }
}
