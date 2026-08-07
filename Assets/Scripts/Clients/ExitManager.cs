using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
    public static ExitManager Instance;
    public List<Transform> exits = new List<Transform>();

    void Awake()
    {
        Instance = this;
        var found = FindObjectsByType<ExitZone>();
        foreach (var p in found) exits.Add(p.transform);
    }

    public Transform GetNearestExit(Vector3 fromPosition)
    {
        if (exits.Count == 0) return null;
        return exits.OrderBy(e => Vector3.Distance(fromPosition, e.position)).First();
    }
}