using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun
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

    [PunRPC]
    public void ActiveAll(bool value)
    {
        for (int i = 0; i < transform.childCount; i++) {
            transform.GetChild(i).gameObject.SetActive(value);
        }
        this.gameObject.SetActive(value);
    }

    [PunRPC]
    public void Inicialize()
    {
        countTime = 0;
        this.transform = gameObject.transform;
        fired = true;
        photonView.RPC("ActiveAll", RpcTarget.All,true);
        
    }
    void Update()
    {
        BulletLife();
    }
    public void Timeout()
    {
        countTime = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        this.gameObject.transform.position = Vector3.zero;
        this.gameObject.SetActive(false);
        this.pool.SetEllement(this.gameObject);
    }

    public void TimeOfArrival(float distance)
    {
        this.distance = distance;
        timeOfArrival = distance / speed;
    }
    public void CombineWithMaic()
    {
        effect.Apply(GetComponentInChildren<Animator>());
    }
    public void CalculateDamage()
    {
    }
    private void DetectCollier()
    {
        if (hit.collider != null)
        {
            //detecta se atingiu o alvo e aplica todas as
            //animações magicas se houver e o dano causado pelo artefato
            //e desativa e retorna a bala para a piscina apos um tempo
            if (timeOfArrival <= 0)
            {
                CombineWithMaic();
                CalculateDamage();
                fired = false;
            }
        }
    }
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
                Timeout();
                return;
            }
            DetectCollier();

            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            transform.position = hit.point;
            Animator ani = GetComponentInChildren<Animator>();

            if (ani.GetCurrentAnimatorStateInfo(0).IsName(animationName) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
            {
                Timeout();

            }
        }
    }
}
