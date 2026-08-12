using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryTrash : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        var sourceSlot = eventData.pointerDrag?.GetComponent<InventorySlotUI>();
        if (sourceSlot != null)
        {
            Inventory.Instance.RemoveItem(sourceSlot.slotIndex);
        }
    }
}