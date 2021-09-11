using UnityEngine;
using Photon.Pun;

public class Explosion : Spells, IEffect
{
    private float maxDuration = 0;
    private GameObject target;
    public override void Use()
    {
        //aqui eu defino a ativação de colisores em áreas tais como os presentes magia
    }
    public void Apply(Animator animator)
    {
        target = animator.gameObject.GetComponentInParent<Bullet>().GetTarget();
        if (!target)
            return;
        this.Use();
        this.animator = animator;
        this.animator.SetTrigger("Applay");
        foreach (var s in status)
        {
            if (s.duration > maxDuration)
                maxDuration = s.duration;
        }
        var e = target.GetComponent<PlayerProperty>();
        //if (e)
        //    e.SetAttribute(this.attibut);
    }
    private void Update()
    {
        maxDuration -= Time.deltaTime;
        if (maxDuration <= 0)
        {
            target.GetComponent<PlayerProperty>().ResetProperty();
        }
    }
}
