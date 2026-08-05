using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public static ExitManager Instance;
    public List<Transform> Exits = new List<Transform>();

    void Awake()
    {
        Instance = this;
    }

    public Transform GetNearestExit(Vector3 fromPosition)
    {
        if (Exits.Count == 0) return null;
        return Exits.OrderBy(e => Vector3.Distance(fromPosition, e.position)).First();
    }
}