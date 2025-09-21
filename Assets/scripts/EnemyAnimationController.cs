using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunningController : MonoBehaviour
{
    private Animator animator;

    [Header("Player Settings")]
    public string playerTag = "Melee"; // Tag for the player
    public float detectionRadius = 0.8f;
    public LayerMask playerLayerMask = -1; // Defaults to all layers

    [Header("Debug")]
    public bool enableDebugLogs = true;
    public bool showDetectionGizmo = true;

    private HashSet<GameObject> nearbyPlayers = new HashSet<GameObject>();

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isRunning", true);

        if (enableDebugLogs)
        {
            Debug.Log($"EnemyRunningController started on {gameObject.name}");
        }
    }

    void Update()
    {
        CheckForPlayers();

        // Debug info
        if (enableDebugLogs && Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log($"Enemy {gameObject.name} state -> Running: {animator.GetBool("isRunning")}, Nearby Players: {nearbyPlayers.Count}");
        }
    }

    void CheckForPlayers()
    {
        nearbyPlayers.Clear();

        // Detect all colliders in the radius
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius, playerLayerMask);

        foreach (Collider col in nearbyColliders)
        {
            if (col.CompareTag(playerTag))
            {
                nearbyPlayers.Add(col.gameObject);
            }
        }

        // Update running state
        bool playerNearby = nearbyPlayers.Count > 0;
        bool currentlyRunning = animator.GetBool("isRunning");

        if (playerNearby && currentlyRunning)
        {
            animator.SetBool("isRunning", false);
            if (enableDebugLogs) Debug.Log($"{gameObject.name} stopped running (player nearby).");
        }
        else if (!playerNearby && !currentlyRunning)
        {
            animator.SetBool("isRunning", true);
            if (enableDebugLogs) Debug.Log($"{gameObject.name} started running (no players nearby).");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showDetectionGizmo)
        {
            Gizmos.color = nearbyPlayers.Count > 0 ? Color.red : Color.green;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }
}
