using UnityEngine;

public class Explosion : Spells, IEffect
{
    public override void Use()
    {
    }
    public void Apply(Animator animator)
    {
        this.Use();
        this.animator = animator;
        this.animator.SetTrigger("Applay");
    }
}
