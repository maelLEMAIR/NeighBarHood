using System;
using UnityEngine;
using TMPro;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private float range = 5.0f;
    [SerializeField] private LayerMask layerMask;
    
    
    [SerializeField] private TMP_Text interactionText;
    
    public Transform cameraTransform;
    private InputManager _inputManager;
    
    private IInteractable _currentInteractable;
    
    private void Start()
    {
        _inputManager = InputManager.Instance;
    }

    private void Update()
    {
        HandleRaycasting();
    }

    private void HandleRaycasting()
    {
        RaycastHit hit;
        if (!Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, range, layerMask)
            || !hit.collider.TryGetComponent(out IInteractable interactable)
            || !interactable.IsActive)
        {
            if (_currentInteractable != null)
            {
                _currentInteractable.OnRelease();
                _currentInteractable = null;
            }

            interactionText.text = "";
            return;
        }

        if (interactable != _currentInteractable)
        {
            _currentInteractable?.OnRelease();
            _currentInteractable = interactable;
            _currentInteractable.OnHit();
        }

        interactionText.text = interactable.InteractionText;

        if (_inputManager.PlayerInteractThisFrame())
            interactable.Interact();
    }
}
