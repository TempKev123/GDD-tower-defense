using UnityEditor;
using UnityEngine;


[ExecuteInEditMode]
public class LockToGrid : MonoBehaviour
{
    public int tileSize = 1;
    public Vector3 tileOffset = Vector3.zero;


    void Update()
    {
        if(!EditorApplication.isPlaying)
        {
            Vector3 currentPosition = transform.position;

            float snappedX = Mathf.Round(currentPosition.x / (tileSize * 2)) * (tileSize * 2) + tileOffset.x;
            float snappedZ = Mathf.Round(currentPosition.z / (tileSize * 2)) * (tileSize * 2) + tileOffset.z;
            float snappedY = Mathf.Round(currentPosition.y / tileSize) * tileSize + tileOffset.y;
            //float snappedY = tileOffset.y; // Preserve the original y-coordinate

            Vector3 snappedPosition = new Vector3(snappedX, snappedY, snappedZ);
            transform.position = snappedPosition;
        }
    }
}