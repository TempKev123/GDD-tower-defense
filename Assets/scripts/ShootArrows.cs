using UnityEngine;
 
public class ShootArrows : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;   // Assign your arrow prefab in Inspector
    public float fireRate = 1f;      // Arrows per second
 
    private float lastFireTime = 0f;
    private float detectionRange = 10000f; // How far ahead to check
    public LayerMask enemyLayer;     // Assign your "Enemy" layer in Inspector
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (IsEnemyInFront())
        {
            if (Time.time - lastFireTime >= 1f / fireRate)
            {
                ShootArrow();
                lastFireTime = Time.time;
            }
        }
    }

    bool IsEnemyInFront()
    {
        // Raycast forward from this object’s position
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, detectionRange, enemyLayer))

        {
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.green); // hit detected
            return true;
        }

        Debug.DrawRay(transform.position, transform.forward * detectionRange, Color.red); // no hit
        return false;
        
    }
 
    void ShootArrow()
    {
        
        // Just spawn the arrow prefab at firePoint
        Vector3 spawnPosition = transform.position +new Vector3(0, 1, 0); // Adjust spawn position if needed
        Instantiate(arrowPrefab, spawnPosition, transform.rotation);
    }
}