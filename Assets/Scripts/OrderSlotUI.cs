using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderSlotUI : MonoBehaviour
{
    public GameObject textInfo;
    public Image[] itemSlots;
    public List<ItemSO> orderedItems;
    public int orderID = 0;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void PlaceItems()
    {
        for (int i = 0; i < orderedItems.Count; i++)
        {
            itemSlots[i].sprite = orderedItems[i].icon;
            itemSlots[i].color = Color.white;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
