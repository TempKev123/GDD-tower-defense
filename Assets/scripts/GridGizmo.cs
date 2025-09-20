using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways] // Draws even in Edit Mode
public class GridGizmo : MonoBehaviour
{
    [Header("Grid Settings")]
    public int gridWidth = 10;   // number of cells across X
    public int gridHeight = 10;  // number of cells across Z
    public float cellSize = 1f;  // size of each tile
    public Vector3 offset = Vector3.zero; // bottom-left corner of grid

    public Color gridColor = Color.green;

    private void OnDrawGizmos()
    {
        Gizmos.color = gridColor;

        // Draw grid lines
        for (int x = 0; x <= gridWidth; x++)
        {
            Vector3 start = offset + new Vector3(x * cellSize, 0, 0);
            Vector3 end = offset + new Vector3(x * cellSize, 0, gridHeight * cellSize);
            Gizmos.DrawLine(start, end);
        }

        for (int z = 0; z <= gridHeight; z++)
        {
            Vector3 start = offset + new Vector3(0, 0, z * cellSize);
            Vector3 end = offset + new Vector3(gridWidth * cellSize, 0, z * cellSize);
            Gizmos.DrawLine(start, end);
        }
    }
}
