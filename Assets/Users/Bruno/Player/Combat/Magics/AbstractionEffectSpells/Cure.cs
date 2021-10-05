using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public class Cure : Spells, IEffect
{
    private GameObject target;
    private RaycastHit hit;
    public float reach;
    public int life;
    public String animationName;

    public override void Aim()
    {
        var origin = transform.position;
        var dir = GetComponentInParent<PlayerController>().cam.transform;
        int mask = LayerMask.GetMask(LayerMask.LayerToName(transform.parent.gameObject.layer));

        Debug.DrawRay(origin, dir.forward * reach, Color.white);
        if (Physics.Raycast(origin, dir.forward, out hit, reach, mask))
        {
            Debug.DrawLine(origin, hit.point, Color.red);
            target = hit.collider.gameObject;
        }
        else
        {
            target = null;
        }
    }
    [PunRPC]
    public override void Use()
    {
        Aim();
        if (target)
        {
            foreach (var s in status)
            {
                GetComponentInParent<PlayerController>().status = s;
            }
            if (!target.layer.Equals(transform.parent.gameObject.layer))
            {
                return;
            }
            GameObject children = target.GetComponentInChildren<Cure>().transform.GetChild(0).gameObject;
            if (!children.activeInHierarchy)
            {
                target.GetComponent<PlayerProperty>().Life = life;
                children.gameObject.SetActive(true);
                var animator = children.GetComponentInChildren<Animator>();
                Apply(animator);
            }

        }
    }
    public void Apply(Animator animator)
    {
        this.animator = animator;
        this.animator.Play(animationName);
    }
    private void Update()
    {
        if (animator)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                animator.gameObject.SetActive(false);
                animator = null;
                GetComponentInParent<PlayerController>().status = null;
            }
        }
    }
}
