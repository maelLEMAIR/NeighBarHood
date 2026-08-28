using UnityEngine;

public class ClientController : MonoBehaviour, IInteractable
{
    public static ClientController CurrentClient { get; private set; }

    private bool _hasTakeMyOrder = false;
    public string InteractionText => "Servir le client";
    
    public bool IsActive => isActiveAndEnabled;

    public void Interact()
    {
        if (!_hasTakeMyOrder)
        {
            Order order = ScriptableObject.CreateInstance<Order>();
            order.clientController = this;
            order.orderedItems = GetComponent<NPCSeatSeeker>()._order;
            OrderManager.Instance.AddOrder(this, order);
            _hasTakeMyOrder  = true;
            return;
        }
        CurrentClient = this;
        NoteBook.Instance.SetTicket(CurrentClient);
        InventoryUIController.Instance.Open(true);
    }

    public void ReceiveItem(ItemSO item)
    {
        Debug.Log($"Le client reçoit {item.nameItem}");
        // logique de satisfaction / argent / etc. ici
        GetComponent<NPCSeatSeeker>().StandUp();
    }

    public void OnHit() { }
    public void OnRelease() { }
}