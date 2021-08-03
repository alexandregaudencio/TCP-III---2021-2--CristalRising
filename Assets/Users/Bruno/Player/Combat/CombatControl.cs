using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatControl : MonoBehaviour, IActive
{
    public float Hertz;
    public int Limit;
    protected int count;

    private void Start()
    {
        Cursor.visible = false;
    }
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
        throw new System.NotImplementedException();
    }
}
