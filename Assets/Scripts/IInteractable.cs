using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public interface IInteractable
{
    public void Interact();

    public void OnHit();
    public void OnRelease();
    
    public string InteractionText { get; }    
    public bool IsActive { get; }
}
