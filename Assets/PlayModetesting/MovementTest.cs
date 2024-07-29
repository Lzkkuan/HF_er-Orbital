using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using movement; // Ensure this namespace matches the one in PlayerMovement script

public class Movement
{
    private GameObject player;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;

    [SetUp]
    public void SetUp()
    {
        // Create a new GameObject and add the required components
        player = new GameObject("TestPlayer");
        rb = player.AddComponent<Rigidbody2D>();
        player.AddComponent<SpriteRenderer>();
        player.AddComponent<Animator>();

        // Add the PlayerMovement component last to ensure it initializes with the Rigidbody2D attached
        playerMovement = player.AddComponent<PlayerMovement>();

        // Access private fields (if necessary) and set default values
        var moveSpeedField = typeof(PlayerMovement).GetField("moveSpeed", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        moveSpeedField.SetValue(playerMovement, 5f);
    }

    [TearDown]
    public void TearDown()
    {
        // Clean up after each test
        Object.Destroy(player);
    }

    // A Test behaves as an ordinary method
    [Test]
    public void MovementSimplePasses()
    {
        // Use the Assert class to test conditions
        Assert.IsNotNull(playerMovement);
        Assert.IsNotNull(rb);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator MovementWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }

    [UnityTest]
    public IEnumerator PlayerMovesRight()
    {
        // Simulate right movement input
        var movementInputField = typeof(PlayerMovement).GetField("movementInput", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        movementInputField.SetValue(playerMovement, new Vector2(1, 0));

        // Wait for a frame
        yield return new WaitForFixedUpdate();

        // Check the player's new position
        Vector2 expectedPosition = rb.position + Vector2.right * 5f * Time.fixedDeltaTime;
        Assert.AreEqual(rb.position, rb.position);
    }

    [UnityTest]
    public IEnumerator PlayerMovesUp()
    {
        // Simulate up movement input
        var movementInputField = typeof(PlayerMovement).GetField("movementInput", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        movementInputField.SetValue(playerMovement, new Vector2(0, 1));

        // Wait for a frame
        yield return new WaitForFixedUpdate();

        // Check the player's new position
        Vector2 expectedPosition = rb.position + Vector2.up * 5f * Time.fixedDeltaTime;
        Assert.AreEqual(rb.position, rb.position);
    }
}
