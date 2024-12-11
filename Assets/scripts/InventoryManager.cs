using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public SlotItem[] slotItems;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void DeselectAllSlots()
    {
        for (int i = 0; i < slotItems.Length; i++)
        {
            slotItems[i].selectedShader.SetActive(false);
        }
    }
}
