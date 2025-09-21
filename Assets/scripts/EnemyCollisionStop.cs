using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionStop : MonoBehaviour
{
    private enemymove moveScript;

    [Header("Target Tag")]
    public string targetTag = "Melee"; // make sure your Player has this tag

    void Start()
    {
        // Get the enemymove script on the same GameObject
        moveScript = GetComponent<enemymove>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            moveScript.speed = 0f; // stop movement
            Debug.Log(gameObject.name + " stopped moving (collided with Player).");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag(targetTag))
        {
            // Optional: let the enemy move again after leaving
            // Remove this block if you want it to stay stopped forever (PvZ style)
            moveScript.speed = 1.5f; // <-- reset to your normal speed
            Debug.Log(gameObject.name + " resumed moving (left Player).");
        }
    }
}
