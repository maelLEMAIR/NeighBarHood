using System;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; private set; }

    [Tooltip("Doit correspondre au nombre de pieceOfInventory de la wheel")]
    public int size = 8;

    public InventorySlot[] slots;

    public event Action<int> OnSlotChanged;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        slots = new InventorySlot[size];
        for (int i = 0; i < size; i++)
            slots[i] = new InventorySlot();
    }

    public bool AddItem(ItemSO item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].IsEmpty)
            {
                slots[i].item = item;
                OnSlotChanged?.Invoke(i);
                return true;
            }
        }

        Debug.Log("Inventaire plein");
        return false;
    }

    public bool RemoveItem(int index)
    {
        if (index < 0 || index >= slots.Length || slots[index].IsEmpty)
            return false;

        slots[index].Clear();
        OnSlotChanged?.Invoke(index);
        return true;
    }

    public bool RemoveItem(ItemSO item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (!slots[i].IsEmpty && slots[i].item == item)
                return RemoveItem(i);
        }
        return false;
    }

    public bool HasItem(ItemSO item)
    {
        foreach (var s in slots)
            if (!s.IsEmpty && s.item == item)
                return true;
        return false;
    }
    
    public void SwapItems(int indexA, int indexB)
    {
        if (indexA == indexB) return;
        if (indexA < 0 || indexA >= slots.Length) return;
        if (indexB < 0 || indexB >= slots.Length) return;

        (slots[indexA].item, slots[indexB].item) = (slots[indexB].item, slots[indexA].item);

        OnSlotChanged?.Invoke(indexA);
        OnSlotChanged?.Invoke(indexB);
    }
}