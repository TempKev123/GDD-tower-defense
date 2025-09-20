using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymove : MonoBehaviour
{
    public int hp;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
    void takeDamage(int damage)
    {
        hp -= damage;
    }
}
