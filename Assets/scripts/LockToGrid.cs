using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteAlways] // works in edit & play mode
public class LockToGridRuntime : MonoBehaviour
{
    [Header("Grid Settings")]
    public int tileSize = 1;
    public Vector3 tileOffset = Vector3.zero;

    private void Update()
    {
#if UNITY_EDITOR
        // Snap in Edit Mode (but only if not playing)
        if (!EditorApplication.isPlaying)
        {
            SnapToGrid();
        }
#endif
        // Snap in Play Mode (e.g. runtime objects that move)
        if (Application.isPlaying)
        {
            SnapToGrid();
        }
    }

    private void SnapToGrid()
    {
        Vector3 currentPosition = transform.position;

        float snappedX = Mathf.Round(currentPosition.x / (tileSize * 2)) * (tileSize * 2) + tileOffset.x;
        float snappedZ = Mathf.Round(currentPosition.z / (tileSize * 2)) * (tileSize * 2) + tileOffset.z;
        float snappedY = Mathf.Round(currentPosition.y / tileSize) * tileSize + tileOffset.y;

        transform.position = new Vector3(snappedX, snappedY, snappedZ);
    }
}
