using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    //public int hp;
    public float speed;
    public int coinvalue;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.z < -4 || transform.position.y < -1)
        {
           Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        spawnmanager.activeEnemies++;
    }

    private void OnDestroy()
    {
        spawnmanager.activeEnemies--;
        GameManager.Instance.AddCoins(coinvalue);
    }
}
