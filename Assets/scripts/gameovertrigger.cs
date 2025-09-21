using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameovertrigger : MonoBehaviour
{
    public GameObject gameOverScreen;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger");
        Debug.Log("Game Over!");
        gameOverScreen.SetActive(true);
        
    }
}
