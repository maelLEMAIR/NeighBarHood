using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    public static InputManager Instance { get; private set;  }

    private PlayerControls _playerControls;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        
        _playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        _playerControls.Enable();
    }
    private void OnDisable()
    {
        _playerControls.Disable();
    }

    public Vector2 GetPlayerMovement()
    {
        return _playerControls.Player.Movement.ReadValue<Vector2>();
    }
    
    public Vector2 GetMouseDelta()
    {
        return _playerControls.Player.Look.ReadValue<Vector2>();
    }
    
    public bool PlayerInteractThisFrame()
    {
        return _playerControls.Player.Interact.WasPressedThisFrame();
    }
    
    public bool PlayerUseInventoryKeyThisFrame()
    {
        return _playerControls.Player.Inventory.WasPressedThisFrame();
    }

    public bool PlayerInteractHeld()
    {
        return _playerControls.Player.Interact.IsPressed();
    }
    
    public bool PlayerSprint()
    {
        return _playerControls.Player.Sprint.IsPressed();
    }
    
    public bool PlayerDropThisFrame()
    {
        return _playerControls.Player.Drop.WasPressedThisFrame();
    }
    
    public bool NextPageKeyPressedThisFrame()
    {
        return _playerControls.Player.NextPage.WasPressedThisFrame();
    }
    
    public bool PreviousPageKeyPressedThisFrame()
    {
        return _playerControls.Player.PreviousPage.WasPressedThisFrame();
    }
    
    public bool PlayerJumpThisFrame()
    {
        return _playerControls.Player.Jump.IsPressed();
    }
}
