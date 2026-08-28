using UnityEngine;

public class TraySocleInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string InteractText = "[E] to place Tray";
    
    public GameObject tray;
    
    private Renderer _socleRenderer;
    private TrayScript _trayScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _trayScript = tray.GetComponent<TrayScript>();
        _socleRenderer = GetComponent<Renderer>();
    }

    public void OnHit()
    {
    }
    
    public void OnRelease()
    {
    }
    
    public void Interact()
    {
        if ( _trayScript.playerHasTray )
        {
            tray.transform.SetParent(transform);
            tray.GetComponent<TrayInteractableScript>().enabled = true;
            tray.GetComponent<TrayScript>().playerHasTray = false;
            tray.transform.localPosition = Vector3.zero;
            tray.transform.localScale = new Vector3(1, 1, 1);
            tray.transform.localRotation = Quaternion.identity;
            _socleRenderer.enabled = false;
            enabled = false;
        }
        else
        {
            Debug.Log("Interact without Tray");
        }
    }
    
    public string InteractionText { get { return InteractText;  }  }
    public bool IsActive => isActiveAndEnabled;
}
