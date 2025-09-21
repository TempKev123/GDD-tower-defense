using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crystalspin : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * 75f * Time.deltaTime);
    }
}
