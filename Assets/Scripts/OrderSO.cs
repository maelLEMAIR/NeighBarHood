using System.Collections.Generic;
using UnityEngine;

public class Order : ScriptableObject
{
    public int orderID = 0;
    public List<ItemSO> orderedItems;
    public ClientController clientController;
}
