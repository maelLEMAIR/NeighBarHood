using System;
using UnityEngine;

public class ExitZone : MonoBehaviour
{
    void Reset()
    {
        var col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out NPCSeatSeeker npc))
        {
            if (npc.GetState() == NPCSeatSeeker.SeekState.Leaving)
            {
                npc.PickNewWanderPoint();
            }
        }
    }
}