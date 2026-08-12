using UnityEngine;

public class TrayScript : MonoBehaviour
{
    public static bool IsInventoryOpen { get; private set; }
    public static bool PlayerHasTray { get; private set; }

    public GameObject inventoryUI;
    public bool playerHasTray = false;

    private bool _isInventoryOpen = false;
    private InputManager _inputManager;
    private Rigidbody _rigidbody;
    
    void Start()
    {
        _inputManager = InputManager.Instance;
    }

    void Update()
    {
        if (playerHasTray)
        {
            if (_inputManager.PlayerUseInventoryKeyThisFrame())
            {
                _isInventoryOpen = !_isInventoryOpen;
                inventoryUI.SetActive(_isInventoryOpen);

                if (_isInventoryOpen)
                    OpenInventory();
                else
                    CloseInventory();
            }
        }
    }   

    void OpenInventory()
    {
        IsInventoryOpen = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void CloseInventory()
    {
        IsInventoryOpen = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}