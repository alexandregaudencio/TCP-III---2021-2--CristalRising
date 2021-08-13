using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    public float speed;
    public float existenceTomeout;
    private float countTime;
    public IEffect effect;
    [HideInInspector]
    public Pool pool;
    private float timeOfArrival;
    private float distance;
    private new Transform transform;
    private bool fired = false;
    public RaycastHit hit;
    [HideInInspector]
    public string animationName;
    private GameObject photonBullet;

    private void Start()
    {
        photonBullet = PhotonView.Find(photonView.ViewID).gameObject;
    }

    [PunRPC]
    public void ActiveAll(bool value)
    {
        GameObject me = PhotonView.Find(photonView.ViewID).gameObject;

        PhotonView.Find(me.GetComponentInChildren<PhotonView>().ViewID).gameObject.SetActive(value);

        if (!value)
        {
            me.transform.position = Vector3.zero;
        }

        me.gameObject.SetActive(value);
    }

    [PunRPC]
    public void Inicialize()
    {
        countTime = 0;
        this.transform = gameObject.transform;
        fired = true;
        photonView.RPC("ActiveAll", RpcTarget.All, true);

    }
    void Update()
    {
        photonView.RPC("BulletLife", RpcTarget.All);
    }
    [PunRPC]
    public void Timeout()
    {
        countTime = 0;

        GetComponent<PhotonView>().RPC("ActiveAll", RpcTarget.All, false);

        pool.photonView.RPC("SetEllement", RpcTarget.All, photonView.ViewID);
    }
    [PunRPC]
    public void TimeOfArrival(float distance)
    {
        Debug.Log("TimeOfArrival");
        this.distance = distance;
        timeOfArrival = distance / speed;
    }
    [PunRPC]
    public void CombineWithMaic()
    {
        var temp = GetComponentInChildren<Animator>();
        //effect.Apply(ViewID);
    }
    public void CalculateDamage()
    {
    }
    [PunRPC]
    private void DetectCollier()
    {
        Debug.Log(hit.point.ToString());
        if (hit.collider != null)
        {
            //detecta se atingiu o alvo e aplica todas as
            //animações magicas se houver e o dano causado pelo artefato
            //e desativa e retorna a bala para a piscina apos um tempo
            Debug.Log(timeOfArrival);
            if (timeOfArrival <= 0)
            {
                if (photonView.IsMine)
                    photonView.RPC("CombineWithMaic", RpcTarget.All);
                //CombineWithMaic();
                CalculateDamage();
                fired = false;
            }
        }
    }
    [PunRPC]
    private void BulletLife()
    {
        //mover a bala pra frente
        if (fired)
        {
            timeOfArrival -= Time.deltaTime;
            countTime += Time.deltaTime;

            // desativa e retorna a bala para a piscina apos um tempo
            if (countTime >= existenceTomeout)
            {
                if (photonView.IsMine)
                    photonView.RPC("Timeout", RpcTarget.All);
                //Timeout();
                return;
            }
            //DetectCollier(); 
            if (photonView.IsMine)
                photonView.RPC("DetectCollier", RpcTarget.All);

            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            //EndAniamtion();
            photonView.RPC("EndAniamtion", RpcTarget.All);
        }
    }
    [PunRPC]
    private void EndAniamtion()
    {
        if (!hit.collider)
        {
            return;
        }
        transform.position = hit.point;
        Animator ani = GetComponentInChildren<Animator>();

        if (ani.GetCurrentAnimatorStateInfo(0).IsName(animationName) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            //Timeout();
            photonView.RPC("Timeout", RpcTarget.All);
        }
    }
}
