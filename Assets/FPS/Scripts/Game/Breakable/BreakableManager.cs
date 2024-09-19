using System.Collections.Generic;
using UnityEngine;

public class BreakableManager : MonoBehaviour
{
    public List<Breakable> breakables = new List<Breakable>();

    void Start()
    {
        CollectBreakables();
    }

    void CollectBreakables()
    {
        breakables.Clear();
        Breakable[] childBreakables = GetComponentsInChildren<Breakable>();
        breakables.AddRange(childBreakables);
    }

    public void AddPushForceToAll(Vector3 fromPosition)
    {
        foreach (Breakable breakable in breakables)
        {
            breakable.AddPushForce(fromPosition);
        }
    }

    public void ActivateAllSubCubes()
    {
        foreach (Breakable breakable in breakables)
        {
            breakable.ActivateSubCubes();
        }
    }

    public int GetBreakableCount()
    {
        return breakables.Count;
    }

    public void AddBreakable(Breakable breakable)
    {
        if (!breakables.Contains(breakable))
        {
            breakables.Add(breakable);
        }
    }

    public void RemoveBreakable(Breakable breakable)
    {
        breakables.Remove(breakable);
    }
}