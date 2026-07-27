using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;

    public static InputManager Instance
    {
        get { return _instance; }
    }

    private PlayerControls _playerControls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
        
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
        return _playerControls.Player.Interact.triggered;
    }

    public bool PlayerInteractHeld()
    {
        return _playerControls.Player.Interact.ReadValue<float>() > 0;
    }
    
    public bool PlayerJumpThisFrame()
    {
        return _playerControls.Player.Jump.triggered;
    }
}
