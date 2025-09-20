using UnityEngine;
 
public class ArcherShooter : MonoBehaviour
{
    [Header("Arrow Settings")]
    public GameObject arrowPrefab;   // Assign your arrow prefab in Inspector
    public Transform firePoint;      // Empty child object at bow tip
    public float fireRate = 1f;      // Arrows per second
 
    private float lastFireTime = 0f;
 
    void Update()
    {
        if (Time.time - lastFireTime >= 1f / fireRate)
        {
            ShootArrow();
            lastFireTime = Time.time;
        }
    }
 
    void ShootArrow()
    {
        // Just spawn the arrow prefab at firePoint
        Instantiate(arrowPrefab, firePoint.position, firePoint.rotation);
    }
}