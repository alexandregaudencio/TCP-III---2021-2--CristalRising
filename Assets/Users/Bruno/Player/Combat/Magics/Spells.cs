using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spells :CombatControl, Neutral, Deffencive
{
    public List<Status> status;
    public Attibute attibut;
    public new AnimationClip animation;
    public virtual void BuildElement()
    {
        throw new System.NotImplementedException();
    }
    public virtual void Use()
    {
        throw new System.NotImplementedException();
    }
    public virtual void CallAnimation() { 
    }
}