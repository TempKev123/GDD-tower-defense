using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationStateController : MonoBehaviour
{
    Animator animator;

    [Header("Enemy Tag")]
    public string enemyTag = "enemy";
    
    [Header("Detection Settings")]
    public float detectionRadius = 0.8f;
    public LayerMask enemyLayerMask = -1; // Default to all layers
    
    [Header("Debug")]
    public bool enableDebugLogs = true;
    public bool showDetectionGizmo = true;

    private HashSet<GameObject> nearbyEnemies = new HashSet<GameObject>();

    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (enableDebugLogs)
        {
            Debug.Log($"Animation controller started on {gameObject.name}");
        }
    }

    void Update()
    {
        CheckForEnemies();
        
        // Debug info
        if (enableDebugLogs && Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log($"Current state - Fighting: {animator.GetBool("isFighting")}, Enemies: {nearbyEnemies.Count}");
            foreach(var enemy in nearbyEnemies)
            {
                Debug.Log($"  - Near enemy: {(enemy ? enemy.name : "NULL")}");
            }
        }
    }

    void CheckForEnemies()
    {
        // Clear previous frame's enemies
        nearbyEnemies.Clear();
        
        // Find all colliders within detection radius
        Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayerMask);
        
        // Filter for enemies
        foreach (Collider col in nearbyColliders)
        {
            if (col.gameObject != gameObject && col.CompareTag(enemyTag))
            {
                nearbyEnemies.Add(col.gameObject);
            }
        }
        
        // Update fighting state
        bool shouldBeFighting = nearbyEnemies.Count > 0;
        bool currentlyFighting = animator.GetBool("isFighting");
        
        if (shouldBeFighting && !currentlyFighting)
        {
            animator.SetBool("isFighting", true);
            if (enableDebugLogs) Debug.Log("Started fighting!");
        }
        else if (!shouldBeFighting && currentlyFighting)
        {
            animator.SetBool("isFighting", false);
            if (enableDebugLogs) Debug.Log("Stopped fighting!");
        }
    }

    void OnDrawGizmosSelected()
    {
        if (showDetectionGizmo)
        {
            // Draw detection radius
            Gizmos.color = nearbyEnemies.Count > 0 ? Color.red : Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
    }

    // Alternative method using trigger colliders (no rigidbodies needed)
    // To use this approach:
    // 1. Add a child GameObject to your player
    // 2. Add a SphereCollider (or other collider) to the child
    // 3. Set the collider to "Is Trigger" = true
    // 4. Scale the collider to your desired detection range
    // 5. Uncomment the methods below and comment out the Update/CheckForEnemies methods above
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            nearbyEnemies.Add(other.gameObject);
            
            if (enableDebugLogs)
            {
                Debug.Log($"Enemy entered detection: {other.name}. Total enemies: {nearbyEnemies.Count}");
            }
            
            if (!animator.GetBool("isFighting"))
            {
                animator.SetBool("isFighting", true);
                if (enableDebugLogs) Debug.Log("Started fighting!");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(enemyTag))
        {
            nearbyEnemies.Remove(other.gameObject);
            
            if (enableDebugLogs)
            {
                Debug.Log($"Enemy left detection: {other.name}. Total enemies: {nearbyEnemies.Count}");
            }
            
            if (nearbyEnemies.Count == 0)
            {
                animator.SetBool("isFighting", false);
                if (enableDebugLogs) Debug.Log("Stopped fighting!");
            }
        }
    }

    void Update()
    {
        // Clean up destroyed enemies
        nearbyEnemies.RemoveWhere(enemy => enemy == null);
        
        // Force stop fighting if no valid enemies
        if (nearbyEnemies.Count == 0 && animator.GetBool("isFighting"))
        {
            animator.SetBool("isFighting", false);
            if (enableDebugLogs) Debug.Log("Force stopped fighting - no enemies detected");
        }
        
        // Debug info
        if (enableDebugLogs && Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log($"Current state - Fighting: {animator.GetBool("isFighting")}, Enemies: {nearbyEnemies.Count}");
        }
    }
    */
}