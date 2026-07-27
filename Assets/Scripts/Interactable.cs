using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    private Outline _outline;
    public string message;
    
    public UnityEvent onInteraction;

    void Start()
    {
        _outline = GetComponent<Outline>();
        DisableOutLine();
    }

    public void Interact()
    {
        onInteraction.Invoke();
    }
    public void DisableOutLine()
    {
        _outline.enabled = false;
    }
    
    public void EnableOutLine()
    {
        _outline.enabled = true;
    }
}
