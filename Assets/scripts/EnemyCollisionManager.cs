using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionManager : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("enemy"))
        {
            Physics.IgnoreCollision(
                GetComponent<Collider>(), 
                collision.collider
            );
        }
    }
}
