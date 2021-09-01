using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Slash : Spells, IEffect
{
    private GameObject target;
    public TrailRenderer trail;
    private float bufferAttack;
    public float damage;
    private void Start()
    {
        bufferAttack = Hertz;
    }
    public override void Use()
    {
        Invoke("Apply",1.0f/10.0f);
        photonView.RPC("UseEffect",RpcTarget.All,target.gameObject.GetComponent<PhotonView>().ViewID);
    }
    [PunRPC]
    private void UseEffect(int targetId) {
        target = PhotonView.Find(targetId).gameObject;
        target.GetComponent<PlayerProperty>().life -= damage;
    }
    public void Apply(Animator animatorId = null)
    {
        trail.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && other.gameObject.layer != gameObject.layer)
        {
            target = other.gameObject;
        }
    }
    private void Update()
    {
        Hertz -= Time.deltaTime;
        if (Hertz < 0) {
            Hertz = bufferAttack;
            trail.enabled = false;
        }
    }
}
