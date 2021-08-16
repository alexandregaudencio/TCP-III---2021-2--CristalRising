using UnityEngine;
using Photon.Pun;

public class Explosion : Spells, IEffect
{
    public override void Use()
    {
    }
    [PunRPC]
    public void Apply(Animator animator)
    {
        this.Use();
        this.animator = animator;
        this.animator.SetTrigger("Applay");
    }
}
