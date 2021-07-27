using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Spells : CombatControl, IPassive
{
    public List<Status> status;
    public Attibute attibut;
    protected Animator animator;
    protected GameObject clone;

    
    public virtual void BuildElement()
    {
        throw new System.NotImplementedException();
    }
}