using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerClickHandler
{
    public int slotIndex;
    public Image icon;

    private Image _background;
    private GameObject _dragIconObj;
    private Canvas _rootCanvas;

    public Color normalColor = Color.white;
    public Color selectedColor = new Color(1f, 0.85f, 0.3f); // jaune, à ajuster

    private void Awake()
    {
        _background = GetComponent<Image>();
        
        Image background = GetComponent<Image>();

        if (background != null)
        {
            background.alphaHitTestMinimumThreshold = 0.1f;
        }
    }

    void OnEnable()
    {
        Inventory.Instance.OnSlotChanged += Refresh;
        InventorySelectionManager.Instance.OnSelectionChanged += RefreshSelection;
        Refresh(slotIndex);
        RefreshSelection(slotIndex);
    }
    
    void RefreshSelection(int changedIndex)
    {
        if (changedIndex != slotIndex) return;

        bool isSelected = InventorySelectionManager.Instance.IsSelected(slotIndex);
        _background.color = isSelected ? selectedColor : normalColor;
    }
    
    void OnDisable()
    {
        if (Inventory.Instance != null)
            Inventory.Instance.OnSlotChanged -= Refresh;
        if (InventorySelectionManager.Instance != null)
            InventorySelectionManager.Instance.OnSelectionChanged -= RefreshSelection;
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
        InventorySelectionManager.Instance.ToggleSlot(slotIndex);
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (Inventory.Instance.slots[slotIndex].IsEmpty)
            return;

        _rootCanvas = GetComponentInParent<Canvas>();

        _dragIconObj = new GameObject("DragIcon");
        _dragIconObj.transform.SetParent(_rootCanvas.transform, false);
        _dragIconObj.transform.SetAsLastSibling();

        var img = _dragIconObj.AddComponent<Image>();
        img.sprite = icon.sprite;
        img.raycastTarget = false; // important : ne doit pas bloquer le OnDrop en dessous

        var rect = _dragIconObj.GetComponent<RectTransform>();
        rect.sizeDelta = icon.rectTransform.sizeDelta;
        _dragIconObj.transform.position = eventData.position;

        icon.color = new Color(1f, 1f, 1f, 0.3f); // estompe l'icône d'origine
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (_dragIconObj != null)
            _dragIconObj.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (_dragIconObj != null)
            Destroy(_dragIconObj);

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