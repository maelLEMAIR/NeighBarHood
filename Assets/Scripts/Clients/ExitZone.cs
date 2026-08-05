using UnityEngine;

public class ExitZone : MonoBehaviour
{
    void Reset()
    {
        // s'assure que le collider est bien en trigger dès l'ajout du composant
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