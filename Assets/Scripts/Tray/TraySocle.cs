using UnityEngine;

public class TraySocle : MonoBehaviour
{
    public bool hasTrayOnIt = true;
    private TraySocleInteractable _traySocleInteractable;
    private Renderer _traySocleRenderer;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _traySocleInteractable = GetComponent<TraySocleInteractable>();
        _traySocleRenderer =  GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_traySocleInteractable.enabled)
            return;
        
        if (transform.childCount == 0)
        {
            hasTrayOnIt = false;
            _traySocleInteractable.enabled = true;
            _traySocleRenderer.enabled = true;
        }
    }
}
