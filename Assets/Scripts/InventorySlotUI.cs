using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public int slotIndex;
    public Image icon;

    private GameObject dragIconObj;
    private Canvas rootCanvas;

    void OnEnable()
    {
        if (Inventory.Instance == null)
        {
            Debug.LogWarning("Inventory.Instance est null — vérifie qu'un GameObject Inventory existe dans la scène.");
            return;
        }

        Inventory.Instance.OnSlotChanged += Refresh;
        Refresh(slotIndex);
    }

    private void Awake()
    {
        Image background = GetComponent<Image>();

        if (background != null)
        {
            background.alphaHitTestMinimumThreshold = 0.1f;
        }
    }
    void OnDisable()
    {
        if (Inventory.Instance != null)
            Inventory.Instance.OnSlotChanged -= Refresh;
    }

    void Refresh(int index)
    {
        if (index != slotIndex) return;

        var slot = Inventory.Instance.slots[slotIndex];
        icon.enabled = !slot.IsEmpty;
        icon.color = Color.white; // reset alpha au cas où on venait de dragger

        if (!slot.IsEmpty)
            icon.sprite = slot.item.icon;
    }
    
    // --- DRAG SOURCE ---

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Click détecté sur slot " + slotIndex);       
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log($"DRAG : {gameObject.name} | slotIndex = {slotIndex}");
        
        if (Inventory.Instance.slots[slotIndex].IsEmpty)
            return;

        rootCanvas = GetComponentInParent<Canvas>();

        dragIconObj = new GameObject("DragIcon");
        dragIconObj.transform.SetParent(rootCanvas.transform, false);
        dragIconObj.transform.SetAsLastSibling();

        var img = dragIconObj.AddComponent<Image>();
        img.sprite = icon.sprite;
        img.raycastTarget = false; // important : ne doit pas bloquer le OnDrop en dessous

        var rect = dragIconObj.GetComponent<RectTransform>();
        rect.sizeDelta = icon.rectTransform.sizeDelta;
        dragIconObj.transform.position = eventData.position;

        icon.color = new Color(1f, 1f, 1f, 0.3f); // estompe l'icône d'origine
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragIconObj != null)
            dragIconObj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (dragIconObj != null)
            Destroy(dragIconObj);

        Refresh(slotIndex); // remet l'icône visible si rien n'a matché de toute façon
    }

    // --- DRAG TARGET (swap) ---

    public void OnDrop(PointerEventData eventData)
    {
        var sourceSlot = eventData.pointerDrag?.GetComponent<InventorySlotUI>();
        if (sourceSlot != null && sourceSlot != this)
        {
            Inventory.Instance.SwapItems(sourceSlot.slotIndex, slotIndex);
        }
    }
}