using System;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string InteractText = "[E] to interact";
    
    private Outline _outline;

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
        Debug.Log("Interact with Cube");
    }
    
    public string InteractionText { get { return InteractText;  }  }
}
