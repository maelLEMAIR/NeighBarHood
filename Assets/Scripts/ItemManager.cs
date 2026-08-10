using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [Header ( "Order Item")]
    [SerializeField] private ItemSO[] allItems;
    private Dictionary<ItemType, ItemSO[]> _items = new Dictionary<ItemType, ItemSO[]>();
    
    private static ItemManager _instance;

    public static ItemManager Instance
    {
        get { return _instance; }
    } 
    
    void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

        BuildItemDictionary();
    }


    private void BuildItemDictionary()
    {
        _items = allItems
            .GroupBy(item => item.type)
            .ToDictionary(g => g.Key, g => g.ToArray());
    }
    
    public ItemSO GetRandomItem(ItemType type)
    {
        ItemSO item = _items[type][Random.Range(0, _items[type].Length)];
        return item;
    }
}
