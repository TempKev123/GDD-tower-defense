using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour
{
    public int unitIndex;
    public UnitDragDrop dragDrop;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => dragDrop.StartDrag(unitIndex));
    }
}
