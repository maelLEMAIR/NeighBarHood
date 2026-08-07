using UnityEngine;

public class WanderPoint : MonoBehaviour
{
    public string Tag = "Default";

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
    }
}