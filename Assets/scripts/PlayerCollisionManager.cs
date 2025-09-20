using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionManager : MonoBehaviour
{
    [Header("Entity that damages the player")]
    public string deadlyTag = "enemy"; // Enemy tag in Unity

    [Header("Player Stats")]
    public int hp = 10; // Player health
    public float damageInterval = 1f; // Time between damage ticks
    public int damagePerTick = 1; // How much damage per tick

    private float lastDamageTime = 0f;

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(deadlyTag))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                TakeDamage(damagePerTick);
                Debug.Log(gameObject.name + " is fighting " + collision.gameObject.name);
                lastDamageTime = Time.time;
            }
        }
    }

    void TakeDamage(int amount)
    {
        hp -= amount;
        Debug.Log(gameObject.name + " took " + amount + " damage. HP left: " + hp);

        if (hp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died!");
        // For player, you might want to trigger respawn, game over UI, etc.
        Destroy(gameObject); 
    }
}
