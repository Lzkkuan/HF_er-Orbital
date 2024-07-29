using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using movement;

public class PlayerIntegrationTest
{
    private GameObject player;
    private GameObject enemy;
    private HealthBar healthBar;

    [SetUp]
    public void Setup()
    {
        // Setup player
        player = new GameObject("Player");
        player.AddComponent<Rigidbody2D>();
        player.AddComponent<SpriteRenderer>();
        player.AddComponent<Animator>(); // Assuming an Animator component is required
        var playerMovement = player.AddComponent<PlayerMovement>();
        var playerAttack = player.AddComponent<PlayerAttack>();
        playerMovement.moveSpeed = 5f;
        playerAttack.knockbackForce = 5;

        // Setup enemy
        enemy = new GameObject("Enemy");
        enemy.AddComponent<BoxCollider2D>();
        var enemyDamageable = enemy.AddComponent<DamageableCharacter>();
        enemy.AddComponent<Rigidbody2D>(); // Ensure the enemy has a Rigidbody2D
        enemyDamageable.health = 100;

        // Setup HealthBar
        healthBar = new GameObject("HealthBar").AddComponent<HealthBar>();
    }

    [UnityTest]
    public IEnumerator PlayerMovesAndAttacks()
    {
        // Simulate player movement
        player.transform.position = Vector3.zero;
        Vector2 targetPosition = new Vector2(5, 0);

        float elapsedTime = 0;
        float duration = 1.0f;

        while (elapsedTime < duration)
        {
            player.GetComponent<PlayerMovement>().moveSpeed = 5f;
            player.GetComponent<Rigidbody2D>().position = Vector2.Lerp(Vector2.zero, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Assert.AreEqual(targetPosition.x, player.transform.position.x, 0.1f);

        // Simulate attack
        enemy.transform.position = player.transform.position + new Vector3(1, 0, 0);
        yield return new WaitForSeconds(0.1f); // Wait for OnTriggerEnter2D to be called

        var damageable = enemy.GetComponent<DamageableCharacter>();
        Assert.IsTrue(damageable.Health < 100); // Check if the enemy's health was reduced
        Assert.AreEqual(healthBar.GetCurrentHp(), damageable.Health); // Check if the health bar was updated correctly

        // Check if knockback was applied
        Vector2 expectedKnockback = new Vector2(5, 0); // Example value
        yield return new WaitForSeconds(0.1f); // Wait for knockback to be applied
        Assert.AreNotEqual(enemy.transform.position, player.transform.position + new Vector3(1, 0, 0)); // Ensure knockback moved the enemy
    }
}

public class HealthBar : MonoBehaviour
{
    private int currentHp;

    public void SetHp(int hp)
    {
        currentHp = hp;
    }

    public int GetCurrentHp()
    {
        return currentHp;
    }
}
