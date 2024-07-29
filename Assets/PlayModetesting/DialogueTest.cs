using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class DialogueManagerTests
{
    private GameObject dialogueManagerObject;
    private DialogueManager dialogueManager;
    private GameObject dialogueBox;
    private Text dialogueText;
    private Text nameText;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and add the DialogueManager component
        dialogueManagerObject = new GameObject("DialogueManager");
        dialogueManager = dialogueManagerObject.AddComponent<DialogueManager>();

        // Create the dialogue UI elements
        dialogueBox = new GameObject("DialogueBox");
        dialogueText = new GameObject("DialogueText").AddComponent<Text>();
        nameText = new GameObject("NameText").AddComponent<Text>();

        // Set up the UI elements in the DialogueManager
        dialogueManager.dialogueBox = dialogueBox;
        dialogueManager.dialogueText = dialogueText;
        dialogueManager.nameText = nameText;

        // Initially set the dialogue box to inactive
        dialogueBox.SetActive(false);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.Destroy(dialogueManagerObject);
        Object.Destroy(dialogueBox);
    }

    [Test]
    public void ShowSoloNPCDialogue()
    {
        // Arrange
        string[] npcLines = { "Hello, traveler!", "How can I help you?" };
        string npcName = "NPC";

        // Act
        dialogueManager.ShowDialogue(npcLines, npcName);

        // Assert
        Assert.IsTrue(dialogueBox.activeInHierarchy);
        Assert.AreEqual(npcLines[0], dialogueText.text);
        Assert.AreEqual(npcName, nameText.text);
    }

    [Test]
    public void ShowConversationDialogue()
    {
        // Arrange
        string[] npcLines = { "Hello, traveler!", "How can I help you?" };
        string[] playerLines = { "Hi, NPC!", "I'm looking for the blacksmith." };
        string npcName = "NPC";
        string playerName = "Player";

        // Act
        dialogueManager.ShowDialogue(npcLines, playerLines, npcName, playerName, true);

        // Assert
        Assert.IsTrue(dialogueBox.activeInHierarchy);
        Assert.AreEqual(npcLines[0], dialogueText.text);
        Assert.AreEqual(npcName, nameText.text);
    }

    [UnityTest]
    public IEnumerator AdvanceSoloNPCDialogue()
    {
        // Arrange
        string[] npcLines = { "Hello, traveler!", "How can I help you?" };
        string npcName = "NPC";
        dialogueManager.ShowDialogue(npcLines, npcName);

        // Act
        yield return null; // Allow some time for the UI to update

        // Simulate pressing the E key
        PressKey(dialogueManager);

        // Wait for a frame
        yield return null; // Allow some time for the UI to update

        // Assert
        Assert.AreEqual(npcLines[1], dialogueText.text);
        Assert.AreEqual(npcName, nameText.text);
    }

    [UnityTest]
    public IEnumerator AdvanceConversationDialogue()
    {
        // Arrange
        string[] npcLines = { "Hello, traveler!", "How can I help you?" };
        string[] playerLines = { "Hi, NPC!", "I'm looking for the blacksmith." };
        string npcName = "NPC";
        string playerName = "Player";
        dialogueManager.ShowDialogue(npcLines, playerLines, npcName, playerName, true);

        // Act
        yield return null; // Allow some time for the UI to update

        // Simulate pressing the E key
        PressKey(dialogueManager);

        // Wait for a frame
        yield return null; // Allow some time for the UI to update

        // Assert
        Assert.AreEqual(playerLines[0], dialogueText.text);
        Assert.AreEqual(playerName, nameText.text);

        // Simulate pressing the E key again
        PressKey(dialogueManager);

        // Wait for a frame
        yield return null; // Allow some time for the UI to update

        // Assert
        Assert.AreEqual(npcLines[1], dialogueText.text);
        Assert.AreEqual(npcName, nameText.text);
    }

    [UnityTest]
    public IEnumerator DialogueBoxTogglesOffAfterLastLine()
    {
        // Arrange
        string[] npcLines = { "Hello, traveler!", "How can I help you?" };
        string npcName = "NPC";
        dialogueManager.ShowDialogue(npcLines, npcName);

        // Act
        yield return null; // Allow some time for the UI to update

        // Simulate pressing the E key to advance to the second line
        PressKey(dialogueManager);
        yield return null; // Allow some time for the UI to update

        // Simulate pressing the E key to advance past the last line
        PressKey(dialogueManager);
        yield return null; // Allow some time for the UI to update

        // Assert
        Assert.IsFalse(dialogueBox.activeInHierarchy);
    }

    private void PressKey(DialogueManager dialogueManager)
    {
        // Directly call the Update method to simulate key press
        dialogueManager.GetType().GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(dialogueManager, null);
    }
}
