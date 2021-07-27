using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public List<GameObject> activeGroup;
    public List<GameObject> inactiveGroup;
    public ScriptableObject projectable;
    public int size;
    void Start()
    {
        activeGroup = new List<GameObject>();
        inactiveGroup = new List<GameObject>();

        for (int i = 0; i < size; i++)
        {
            inactiveGroup.Add((projectable as ProjectableFactory).BulletFactory(this));
        }
    }

    private bool HalfEmpty()
    {
        if (inactiveGroup.Count > 1)
        {
            return true;
        }
        return false;
    }

    internal void SetEllement(GameObject go)
    {
        inactiveGroup.Add(go);
        activeGroup.Remove(go);
    }

    public GameObject GetEllement()
    {
        GameObject go;
        if (!HalfEmpty())
        {
            inactiveGroup.Add((projectable as ProjectableFactory).BulletFactory(this));
        }
        go = inactiveGroup[0];
        inactiveGroup.Remove(go);
        activeGroup.Add(go);
        return go;
    }
}
