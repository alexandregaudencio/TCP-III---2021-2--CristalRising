using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatControl :MonoBehaviour, Offencive
{
    public int Hertz;
    public int Limit;
    protected int count;
    public virtual void Ain()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Fire()
    {
        throw new System.NotImplementedException();
    }

    public virtual void Reload()
    {
        throw new System.NotImplementedException();
    }
}
