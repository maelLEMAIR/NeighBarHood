using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public static InventoryUIController Instance { get; private set; }
    public static bool IsOpen { get; private set; }

    public GameObject inventoryUI;
    public GameObject serveButton;   // le bouton "Servir", désactivé par défaut
    public TrayScript trayScript; // référence assignée dans l'Inspector
    
    public GameObject noteBook;
    private Vector3 _firstLocalPosNoteBook;

    private InputManager _inputManager;

    void Awake() => Instance = this;

    void Start()
    {
        _inputManager = InputManager.Instance;
        
        _firstLocalPosNoteBook = noteBook.transform.localPosition;
    }

    void Update()
    {
        if (!_inputManager.PlayerUseInventoryKeyThisFrame())
            return;

        if (IsOpen)
        {
            Close();
        }
        else if (trayScript.playerHasTray)
        {
            Open(false);
        }
    }

    public void Open(bool showServeButton = false)
    {
        inventoryUI.SetActive(true);
        serveButton.SetActive(showServeButton);

        noteBook.transform.localPosition = new Vector3(
            455.0f, 0.0f, noteBook.transform.localPosition.z
            );
        
        IsOpen = true;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Close()
    {
        inventoryUI.SetActive(false);
        serveButton.SetActive(false);

        noteBook.transform.localPosition = _firstLocalPosNoteBook;
        
        IsOpen = false;

        InventorySelectionManager.Instance.ClearSelection();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}