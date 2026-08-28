using System;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class InventorySelectionManager : MonoBehaviour
{
    public static InventorySelectionManager Instance { get; private set; }

    private readonly HashSet<int> _selectedSlots = new HashSet<int>();

    public IReadOnlyCollection<int> SelectedSlots => _selectedSlots;

    // notifie quel slot précis a changé d'état (sélectionné/désélectionné)
    public event Action<int> OnSelectionChanged;

    void Awake()
    {
        Instance = this;
    }

    public bool IsSelected(int index) => _selectedSlots.Contains(index);

    public void ToggleSlot(int index)
    {
        if (Inventory.Instance.slots[index].IsEmpty) return;

        if (_selectedSlots.Contains(index))
            _selectedSlots.Remove(index);
        else
            _selectedSlots.Add(index);

        OnSelectionChanged?.Invoke(index);
    }

    public void ClearSelection()
    {
        var toClear = new List<int>(_selectedSlots);
        _selectedSlots.Clear();

        foreach (int index in toClear)
            OnSelectionChanged?.Invoke(index);
    }
}