using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float _sensitivity;

    public Transform playerBody;
    
    private InputManager _inputManager;
    
    private Vector2 _mMouseInput;

    private float _xRotation = 0.0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inputManager = InputManager.Instance;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 mouseInput = _inputManager.GetMouseDelta() * _sensitivity;

        _xRotation -= mouseInput.y;
        _xRotation = Mathf.Clamp(_xRotation, -89f, 89f);

        transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseInput.x);
    }
}
