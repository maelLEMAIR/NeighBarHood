using System;
using System.Collections.Generic;
using UnityEngine;

public class ChairManager : MonoBehaviour
{
    public static ChairManager Instance;
    public List<ChairSlot> chairs = new List<ChairSlot>();

    void Awake()
    {
        Instance = this;
        chairs.AddRange(FindObjectsByType<ChairSlot>());
    }

    public ChairSlot RequestChair(GameObject npc)
    {
        foreach (var chair in chairs)
        {
            if (chair.state == ChairState.Free)
            {
                chair.Reserve(npc); // réservée immédiatement, avant même de bouger
                return chair;
            }
        }
        return null; // aucune chaise libre
    }
}