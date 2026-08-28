using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ClientOrder
{
    public ClientController Client { get; private set; }
    public Order Order { get; private set; }
    public GameObject OrderPrefab { get; private set; }

    public ClientOrder(ClientController client, Order order, GameObject orderPrefab)
    {
        Client = client;
        Order = order;
        OrderPrefab = orderPrefab;
    }
}

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance { get; private set; }

    public GameObject prefabTicket;
    public Transform orderContainer;

    public List<ClientOrder> Orders {  get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        Orders = new List<ClientOrder>();
    }

    public void AddOrder(ClientController client, Order order)
    {
        if (order == null)
        {
            Debug.LogWarning("Order is null");
            return;
        }

        order.orderID = Orders.Count;
        
        GameObject newOrder = Instantiate(prefabTicket, orderContainer);
        newOrder.transform.localPosition = Vector3.zero;
        newOrder.SetActive(false);
        
        OrderSlotUI newOrderSlotUi = newOrder.GetComponent<OrderSlotUI>();
        newOrderSlotUi.orderedItems = order.orderedItems;
        newOrderSlotUi.orderID = order.orderID;
        newOrderSlotUi.textInfo.GetComponent<TextMeshProUGUI>().text = newOrderSlotUi.orderID.ToString();
        newOrderSlotUi.PlaceItems();
        
        ClientOrder clientOrder = new ClientOrder(client, order, newOrder);
        Orders.Add(clientOrder);
    }

    public void RemoveOrder(ClientController client)
    {
        int index = Orders.FindIndex(co => co.Client == client);
        if (index == -1)
            return;

        Orders.RemoveAt(index);
    }

    // Bonus : retrouver facilement l'order d'un client
    public Order GetOrderByClient(ClientController client)
    {
        ClientOrder found = Orders.Find(co => co.Client == client);
        return found?.Order;
    }
}