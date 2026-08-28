using System.Collections.Generic;
using UnityEngine;

public class ServeButton : MonoBehaviour
{
    public void OnServeClicked()
    {
        var selected = new List<int>(InventorySelectionManager.Instance.SelectedSlots);

        if (selected.Count == 0)
        {
            Debug.Log("Aucun item sélectionné");
            return;
        }

        var client = ClientController.CurrentClient;
        if (client == null) return;

        foreach (int index in selected)
        {
            var item = Inventory.Instance.slots[index].item;
            if (item != null)
                client.ReceiveItem(item);
        }

        // on retire du plus grand index au plus petit pour éviter
        // que la suppression d'un slot ne décale les indices restants
        selected.Sort((a, b) => b.CompareTo(a));
        foreach (int index in selected)
            Inventory.Instance.RemoveItem(index);

        InventorySelectionManager.Instance.ClearSelection();
        InventoryUIController.Instance.Close();
    }
}