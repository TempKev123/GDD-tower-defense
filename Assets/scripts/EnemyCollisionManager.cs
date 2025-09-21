using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour
{
    [Header("Entity that damages this one")]
    private string deadlyTag = "Melee"; // Change this in Inspector

    [Header("Enemy Stats")]
    public int hp = 5; // Enemy health
    public float damageInterval = 1f; // Time (in seconds) between damage ticks

    private float lastDamageTime = 0f;

    void OnCollisionEnter(Collision collision)
    {
        // Ignore collisions between enemies
        if (collision.gameObject.CompareTag("enemy"))
        {
            Physics.IgnoreCollision(
                GetComponent<Collider>(),
                collision.collider
            );
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("projectile"))
        {
            TakeDamage(1);
            Debug.Log(gameObject.name + " hit by projectile");
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag(deadlyTag))
        {
            if (Time.time - lastDamageTime >= damageInterval)
            {
                TakeDamage(1);
                Debug.Log(gameObject.name + " taking damage ");
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
        Destroy(gameObject);
    }
}
