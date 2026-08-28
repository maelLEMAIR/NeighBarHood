using System;
using UnityEngine;

public class NoteBook : MonoBehaviour
{
    public static NoteBook Instance {  get; private set; } 
    
    private OrderManager _orderManager;
    private InputManager _inputManager;
    
    private GameObject _currentTicket;
    private int _indexTicket = 0;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _orderManager = OrderManager.Instance;
        _inputManager = InputManager.Instance;
        _currentTicket = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTicket == null && _orderManager.Orders.Count != 0)
        {
            _currentTicket = _orderManager.Orders[_indexTicket].OrderPrefab;
            _currentTicket.SetActive(true);
        }

        if (_currentTicket == null)
            return;
        
        if (_inputManager.NextPageKeyPressedThisFrame())
            ChangeTicket(1);
        if (_inputManager.PreviousPageKeyPressedThisFrame())
            ChangeTicket(-1);
    }

    void ChangeTicket(int index)
    {
        if (_orderManager.Orders.Count <= 1)
            return;
        
        if (_indexTicket + index >= _orderManager.Orders.Count)
            _indexTicket = 0;
        else if ( _indexTicket + index < 0 )
            _indexTicket = _orderManager.Orders.Count - 1;
        else
            _indexTicket += index;
            
        _currentTicket.SetActive(false);
        _currentTicket = _orderManager.Orders[_indexTicket].OrderPrefab;
        _currentTicket.SetActive(true);
    }

    public void SetTicket(ClientController client)
    {
        for (int i = 0; i < _orderManager.Orders.Count; i++)
            if (_orderManager.Orders[i].Client == client)
                _indexTicket = i;
              
        _currentTicket.SetActive(false);
        _currentTicket = _orderManager.Orders[_indexTicket].OrderPrefab;
        _currentTicket.SetActive(true);
    }
}
