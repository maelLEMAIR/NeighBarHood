using System;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public static Hand Instance { get; private set; }
    
    private InputManager _inputManager;
    
    void Start()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _inputManager = InputManager.Instance;
    }

    private void Update()
    {
        if (transform.childCount == 0)
            return;

        if (_inputManager.PlayerDropThisFrame())
        {
            Transform transformChild = transform.GetChild(0).gameObject.transform;
            transformChild.SetParent(null);
            transformChild.localScale = Vector3.one;
            transformChild.rotation = Quaternion.identity;
            transformChild.GetComponent<WorldItem>().enabled = true;
            transformChild.GetComponent<BoxCollider>().isTrigger = false;
            transformChild.GetComponent<Rigidbody>().isKinematic = false;
            
        }
    }
}
