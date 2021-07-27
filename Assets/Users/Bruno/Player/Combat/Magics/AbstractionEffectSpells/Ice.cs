using UnityEngine;

public class Ice : Spells, IEffect
{
    public override void Use()
    {
    }
    public void Apply(Transform target)
    {
        this.Use();
        this.animator = target.GetComponentInChildren<Animator>();
        this.animator.SetTrigger("Applay");
    }
}
