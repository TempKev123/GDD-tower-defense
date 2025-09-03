using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotatecamera : MonoBehaviour
{
    public float rotationspeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float hInput=Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up,hInput*rotationspeed*Time.deltaTime);
        
    }
}
