using UnityEngine;
using UnityEngine.AI;

public enum ChairState { Free, Reserved, Occupied }

public class ChairSlot : MonoBehaviour
{
    public ChairState state = ChairState.Free;
    public Transform seatPoint;
    public GameObject socupant;

    private NavMeshObstacle chairObstacle;

    void Awake()
    {
        seatPoint = transform;
        chairObstacle = GetComponent<NavMeshObstacle>();
    }

    public void Reserve(GameObject npc)
    {
        state = ChairState.Reserved;
        socupant = npc;

        // désactive le carving de la CHAISE le temps de l'approche
        if (chairObstacle != null) chairObstacle.enabled = false;
    }

    public void Occupy(GameObject npc)
    {
        state = ChairState.Occupied;
        socupant = npc;

        // le PNJ est assis -> réactive le carving de la chaise
        if (chairObstacle != null) chairObstacle.enabled = true;
    }

    public void Release()
    {
        state = ChairState.Free;
        socupant = null;

        // le carving de la chaise reste actif, rien à changer ici
    }
}