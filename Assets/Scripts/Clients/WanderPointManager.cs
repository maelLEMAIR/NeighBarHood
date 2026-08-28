using System.Collections.Generic;
using UnityEngine;

public class WanderPointManager : MonoBehaviour
{
    public static WanderPointManager Instance;
    public List<Transform> points = new List<Transform>();

    void Awake()
    {
        Instance = this;

        if (points.Count == 0)
        {
            var found = FindObjectsByType<WanderPoint>(FindObjectsInactive.Exclude);
            foreach (var p in found) points.Add(p.transform);
        }
    }

    public Transform GetRandomPoint()
    {
        if (points.Count == 0) return null;
        return points[Random.Range(0, points.Count)];
    }

    public Transform GetRandomPointExcluding(Transform exclude)
    {
        if (points.Count <= 1) return GetRandomPoint();

        Transform point;
        do
        {
            point = points[Random.Range(0, points.Count)];
        } while (point == exclude);

        return point;
    }
}