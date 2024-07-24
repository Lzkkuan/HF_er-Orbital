using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IDialogueManager : MonoBehaviour
{
    public static IDialogueManager instance; // 单例实例

    public Text nameText;
    public Text dialogueText;
    public GameObject dialogueBox; // 对话框的引用
    private Queue<string> sentences;
    private bool isDialogueActive = false;

    void Awake()
    {
        // 确保只有一个实例存在
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false); // 在开始时隐藏对话框
    }

    public void StartDialogue(Dialogue dialogue)
    {
        if (isDialogueActive)
        {
            Debug.LogWarning("Dialogue is already active. Cannot start a new one.");
            return;
        }

        Debug.Log("Starting dialogue with " + dialogue.name);
        nameText.text = dialogue.name;
        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        dialogueBox.SetActive(true); // 显示对话框
        isDialogueActive = true;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        StartCoroutine(DisplayNextSentenceCoroutine());
    }

    IEnumerator DisplayNextSentenceCoroutine()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            yield break;
        }
        string sentence = sentences.Dequeue();
        Debug.Log("Dequeued sentence: " + sentence); // 确认句子已被取出
        dialogueText.text = sentence;
        Debug.Log("Displaying sentence in UI: " + sentence); // 确认句子已被显示在UI中

        // 确保UI有足够时间更新
        yield return null;
        yield return new WaitForSeconds(0.4f); // 可以调整这个值以适应不同情况
    }

    void EndDialogue()
    {
        Debug.Log("End of conversation");
        dialogueBox.SetActive(false); // 隐藏对话框
        isDialogueActive = false;
    }

    public bool IsDialogueBoxActive()
    {
        return dialogueBox.activeSelf; // 检查对话框是否处于活动状态
    }
}
