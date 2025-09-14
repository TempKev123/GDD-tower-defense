using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndPlace : MonoBehaviour, IPointerClickHandler
{
    [Header("Card Data")]
    public CardScriptableObject cardData;   // Assigned by CardHolder

    private Camera cam;
    private bool isPlacing = false;

    private void Awake()
    {
        cam = Camera.main;
    }

    // First click on UI card → enter placement mode
    public void OnPointerClick(PointerEventData eventData)
    {
        if (cardData != null && cardData.prefab != null)
        {
            isPlacing = true;
            Debug.Log("Placement mode active for: " + cardData.name);
        }
    }

    private void Update()
    {
        if (isPlacing && Input.GetMouseButtonDown(0)) // left click in world
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Spawn the prefab where clicked
                Instantiate(cardData.prefab, hit.point, Quaternion.identity);
                Debug.Log("Placed " + cardData.name + " at " + hit.point);

                isPlacing = false; // exit placement mode
            }
        }
    }
}
