using UnityEngine;
using UnityEngine.UI;

public class UnitDragDrop : MonoBehaviour
{
    public Camera cam;                 // กล้องหลัก
    public GameObject[] unitPrefabs;   // ใส่ prefab ยูนิต (Knight, Wizard, etc.)
    private GameObject ghostUnit;      // ยูนิตเงา (ตอนลาก)
    private GameObject currentUnit;    // ยูนิตจริง
    private int currentIndex;

    void Awake()
    {
        if (!cam) cam = Camera.main;
    }

    void Update()
    {
        if (ghostUnit != null)
        {
            // ให้ยูนิตเงาตามเมาส์
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            ghostUnit.transform.position = pos;

            // ปล่อยเมาส์ซ้าย = วางยูนิต
            if (Input.GetMouseButtonUp(0))
            {
                PlaceUnit();
            }
        }
    }

    // เรียกจากปุ่ม UI
    public void StartDrag(int index)
    {
        currentIndex = index;
        currentUnit = unitPrefabs[index];

        // สร้างยูนิตเงาแบบโปร่งใส
        ghostUnit = Instantiate(currentUnit);
        var sr = ghostUnit.GetComponentInChildren<SpriteRenderer>();
        if (sr) sr.color = new Color(1f, 1f, 1f, 0.5f);
    }

    void PlaceUnit()
    {
        // ตรวจว่าเมาส์อยู่เหนือ Grass หรือเปล่า
        Collider2D hit = Physics2D.OverlapPoint(ghostUnit.transform.position);
        if (hit != null && hit.CompareTag("Grass"))
        {
            // วางยูนิตจริงที่ตำแหน่งของ Grass (snap ตรงกลาง)
            Instantiate(currentUnit, hit.transform.position, Quaternion.identity);
        }

        // ลบ ghost
        Destroy(ghostUnit);
        ghostUnit = null;
    }
}
