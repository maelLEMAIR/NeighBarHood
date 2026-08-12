[System.Serializable]
public class InventorySlot
{
    public ItemSO item;

    public bool IsEmpty => item == null;

    public void Clear()
    {
        item = null;
    }
}