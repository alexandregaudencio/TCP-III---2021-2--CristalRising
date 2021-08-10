using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public abstract class CombatControl : MonoBehaviourPun, IActive
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
