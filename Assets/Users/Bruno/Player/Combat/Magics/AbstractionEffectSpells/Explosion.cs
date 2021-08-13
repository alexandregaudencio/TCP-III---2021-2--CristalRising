using UnityEngine;
using Photon.Pun;

public class Explosion : Spells, IEffect
{
    public override void Use()
    {
    }
    [PunRPC]
    public void Apply(int animatorId)
    {
        this.Use();
        this.animator = PhotonView.Find(animatorId).GetComponent<Animator>();
        //this.animator = animator;
        this.animator.SetTrigger("Applay");
    }
}
