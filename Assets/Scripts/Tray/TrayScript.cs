using UnityEngine;

public class TrayScript : MonoBehaviour
{
    public static bool PlayerHasTray { get; private set; }

    public bool playerHasTray = false;

    void Update()
    {
        PlayerHasTray = playerHasTray;
    }
     
}