using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{ 
    public float playerReach = 3.0f;
    private Interactable _currentInteractable;

    public Transform cameraTransform;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void CheckInteraction()
    {
        
        RaycastHit hit;
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            Debug.DrawRay(ray.origin, ray.direction * playerReach, Physics.Raycast(ray, out hit, playerReach) ? Color.green : Color.red);
            if (hit.collider.CompareTag("Interactable"))
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();

                if (_currentInteractable && newInteractable != _currentInteractable)
                {
                    _currentInteractable.DisableOutLine();
                }
                
                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else
                {
                    DisableCurrentInteractable();
                }
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable interactable)
    {
        _currentInteractable = interactable;
        _currentInteractable.EnableOutLine();
    }
    
    void DisableCurrentInteractable()
    {
        if (_currentInteractable)
        {
            _currentInteractable.DisableOutLine();
            _currentInteractable = null;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
    }
}
