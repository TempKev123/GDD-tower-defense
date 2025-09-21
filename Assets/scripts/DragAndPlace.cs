using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

#if UNITY_EDITOR
using UnityEditor;
#endif

// [ExecuteAlways] ❌ เอาออก เพื่อกันไม่ให้ UI โดน snap ตอนแก้ใน Scene
public class DragAndPlace : MonoBehaviour, IPointerClickHandler
{
    [Header("Card Data")]
    public CardScriptableObject cardData;   // Assigned by CardHolder

    private Camera cam;
    private bool isPlacing = false;
    private GameObject previewModel;

    [Header("Grid Settings")]
    public int tileSize = 1;                 
    public Vector3 tileOffset = Vector3.zero;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
#if UNITY_EDITOR
        // Snap ใน editor เฉพาะ object ที่ไม่ใช่ UI
        if (!EditorApplication.isPlaying && !(transform is RectTransform))
        {
            SnapToGrid(transform);
        }
#endif

        if (Application.isPlaying && isPlacing && previewModel != null)
        {
            // preview ติดตามเมาส์
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Vector3 snappedPos = GetSnappedPosition(hit.point);
                previewModel.transform.position = snappedPos;
            }

            // ยืนยันการวาง
            if (Input.GetMouseButtonDown(0))
            {
                FinalizePlacement();
            }

            // ยกเลิกการวาง
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                CancelPlacement();
            }
        }
    }

    // คลิกการ์ดเพื่อเริ่มวาง
    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardData == null || cardData.prefab == null) return;

        if (!GameManager.Instance.CanAfford(cardData.cost))
        {
            Debug.Log("Not enough coins for: " + cardData.name);
            return;
        }

        if (!isPlacing)
        {
            previewModel = Instantiate(cardData.prefab);
            MakeTransparent(previewModel, 0.5f);
            isPlacing = true;
            Debug.Log("Preview active for: " + cardData.name);
        }
    }

    // วางจริง
    private void FinalizePlacement()
    {
        if (previewModel != null)
        {
            if (GameManager.Instance.CanAfford(cardData.cost))
            {
                GameManager.Instance.SpendCoins(cardData.cost);

                MakeTransparent(previewModel, 1f);
                previewModel = null;
                isPlacing = false;

                Debug.Log("Placed " + cardData.name);
            }
            else
            {
                Debug.Log("Not enough coins at placement!");
                CancelPlacement();
            }
        }
    }

    // ยกเลิก
    private void CancelPlacement()
    {
        if (previewModel != null)
        {
            Destroy(previewModel);
            previewModel = null;
            isPlacing = false;
            Debug.Log("Placement cancelled for: " + cardData.name);
        }
    }

    // --- Helpers ---
    private void MakeTransparent(GameObject obj, float alpha)
    {
        foreach (Renderer r in obj.GetComponentsInChildren<Renderer>())
        {
            if (r.material.HasProperty("_Color"))
            {
                Color c = r.material.color;
                c.a = alpha;
                r.material.color = c;
            }
        }
    }

    private void SnapToGrid(Transform t)
    {
        // ข้ามถ้าเป็น UI (RectTransform)
        if (t is RectTransform) return;

        t.position = GetSnappedPosition(t.position);
    }

    private Vector3 GetSnappedPosition(Vector3 pos)
    {
        float snappedX = Mathf.Round((pos.x - tileOffset.x) / (tileSize * 2)) * (tileSize * 2) + tileOffset.x;
        float snappedZ = Mathf.Round((pos.z - tileOffset.z) / (tileSize * 2)) * (tileSize * 2) + tileOffset.z;

        // ตายตัวบนพื้น grid
        float snappedY = tileOffset.y;

        return new Vector3(snappedX, snappedY, snappedZ);
    }
}
