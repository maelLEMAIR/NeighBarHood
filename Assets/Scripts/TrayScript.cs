using UnityEngine;

public class TrayScript : MonoBehaviour, IInteractable
{
    [SerializeField] private string InteractText = "[E] to take";
    
    private Outline _outline;

    public Transform parentToAttach;

    private void Start()
    {
        _outline = GetComponent<Outline>();
        DisableOutLine();
    }

    public void DisableOutLine()
    {
        _outline.enabled = false;
    }
    
    public void EnableOutLine()
    {
        _outline.enabled = true;
    }

    public void OnHit()
    {
        EnableOutLine();
    }
    
    public void OnRelease()
    {
        DisableOutLine();
    }

    public void Interact()
    {
        Debug.Log("Interact with Tray");
        transform.SetParent(parentToAttach);
        transform.localPosition = new Vector3(0.25f, -0.4f, 0.25f);
        transform.localRotation = Quaternion.identity;
        this.enabled = false;
    }
    
    public string InteractionText { get { return InteractText;  }  }
    public bool IsActive => isActiveAndEnabled;
}
