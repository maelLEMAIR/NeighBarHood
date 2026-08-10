using UnityEngine;

public class CreateInventoryWheel : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public GameObject[] pieceOfInventory;
    void Start()
    {
        for (int i = 0; i < pieceOfInventory.Length; i++)
        {
            float angle = i * (360f / pieceOfInventory.Length);

            pieceOfInventory[i].transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
