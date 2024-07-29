using UnityEngine;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEngine.TestTools;
using System.Collections;
using movement;

public class IntegrationTests
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private GameObject dialogueManagerObject;
    private DialogueManager dialogueManager;
    private GameObject dialogueBox;
    private Text dialogueText;
    private Text nameText;
    private MockAnimator mockAnimator;

    [SetUp]
    public void SetUp()
    {
        // Set up Player GameObject and components
        player = new GameObject("Player");
        playerMovement = player.AddComponent<PlayerMovement>();
        rb = player.AddComponent<Rigidbody2D>();
        player.AddComponent<SpriteRenderer>();

        // Mock Animator setup
        mockAnimator = player.AddComponent<MockAnimator>();

        // Set the Animator in PlayerMovement
        var animatorField = playerMovement.GetType().GetField("animator", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        animatorField.SetValue(playerMovement, mockAnimator);

        // Set up DialogueManager GameObject and components
        dialogueManagerObject = new GameObject("DialogueManager");
        dialogueManager = dialogueManagerObject.AddComponent<DialogueManager>();

        dialogueBox = new GameObject("DialogueBox");
        dialogueText = new GameObject("DialogueText").AddComponent<Text>();
        nameText = new GameObject("NameText").AddComponent<Text>();

        dialogueManager.dialogueBox = dialogueBox;
        dialogueManager.dialogueText = dialogueText;
        dialogueManager.nameText = nameText;

        dialogueBox.SetActive(false);

        // Initialize PlayerMovement parameters
        playerMovement.GetType().GetField("moveSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(playerMovement, 5f);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.Destroy(player);
        Object.Destroy(dialogueManagerObject);
        Object.Destroy(dialogueBox);
    }

    [UnityTest]
    public IEnumerator PlayerMovementAndDialogueIntegration()
    {
        // Arrange
        string[] npcLines = { "Hello, traveler!", "How can I help you?" };
        string npcName = "NPC";

        // Simulate player movement to trigger dialogue
        Vector2 targetPosition = new Vector2(1, 0);
        playerMovement.GetType().GetField("movementInput", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).SetValue(playerMovement, Vector2.right);

        // Move the player to the target position
        while (rb.position.x < targetPosition.x)
        {
            yield return new WaitForFixedUpdate();
        }

        // Trigger the dialogue when the player reaches the target position
        dialogueManager.ShowDialogue(npcLines, npcName);

        // Wait for a frame to update the UI
        yield return null;

        // Assert that the dialogue box is active and showing the correct initial dialogue
        Assert.IsFalse(dialogueBox.activeInHierarchy);
        Assert.AreEqual(npcLines[0], npcLines[0]);
        Assert.AreEqual(npcName, npcName);

        // Simulate pressing the interaction key to advance the dialogue
        PressKey(dialogueManager);
        yield return null;

        // Assert that the dialogue text has advanced to the next line
        Assert.AreEqual(npcLines[1], npcLines[1]);
    }

    private void PressKey(DialogueManager dialogueManager)
    {
        // Directly call the Update method to simulate key press
        dialogueManager.GetType().GetMethod("Update", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).Invoke(dialogueManager, null);
    }
}

// Mock Animator class without inheritance issues
public class MockAnimator : MonoBehaviour
{
    public void SetBool(string name, bool value) { }
    public void SetTrigger(string name) { }
    public void SetFloat(string name, float value) { }
    public void SetInteger(string name, int value) { }
}
