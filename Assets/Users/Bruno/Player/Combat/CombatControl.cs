using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatControl : MonoBehaviour, IActive
{
    public int Hertz;
    public int Limit;
    protected int count;

    public virtual void Aim()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Reload()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Use()
    {
        Debug.LogWarning("função User na classe CombatControl esta sendo chamada.");
        //throw new System.NotImplementedException();
    }
}
