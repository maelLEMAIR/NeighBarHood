using UnityEngine;

public class WorldItem : MonoBehaviour, IInteractable
{
    public ItemSO item;

    public string InteractionText => $"Ramasser {item.nameItem}";
    public bool IsActive { get; private set; } = true;

    public void Interact()
    {
        if ( Inventory.Instance == null)
            Debug.Log("Inventory instance is null");

        if (!TrayScript.PlayerHasTray)
        {
            transform.SetParent(Hand.Instance.gameObject.transform);
            transform.localPosition = new Vector3(0.25f, -0.4f, 0.25f);
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            transform.localRotation = Quaternion.identity;
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<Rigidbody>().isKinematic = true;
            enabled = false;
            return;
        }
        
        if (Inventory.Instance.AddItem(item))
        {
            IsActive = false;
            Destroy(gameObject);
        }
    }

    public void OnHit() { }
    public void OnRelease() { }
}