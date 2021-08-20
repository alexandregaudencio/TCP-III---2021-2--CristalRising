using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPun, Damage
{
    public float speed;
    public float damage;
    public float existenceTomeout;
    private float countTime;
    public IEffect effect;
    [HideInInspector]
    public Pool pool;
    private float timeOfArrival;
    private float distance;
    private new Transform transform;
    private bool fired = false;
    public Vector3 hit;
    [HideInInspector]
    public string animationName;
    [HideInInspector]
    public GameObject target { private get; set; }

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
    public void Inicialize(Vector3 point, float timeOfArrival, Vector3 pos, Vector3 rot, int targetId)
    {
        target = PhotonView.Find(targetId).gameObject;
        pool.Out(photonView.ViewID);
        pool.ActiveInstance();
        hit = point;
        countTime = 0;
        this.transform = gameObject.transform;
        fired = true;
        TimeOfArrival(timeOfArrival);
        transform.SetPositionAndRotation(pos, Quaternion.Euler(rot));
        ActiveAll(true);

    }
    void Update()
    {
        BulletLife();
    }
    public void Timeout()
    {
        countTime = 0;
        pool.In(photonView.ViewID);
        ActiveAll(false);

    }
    public void TimeOfArrival(float distance)
    {
        this.distance = distance;
        timeOfArrival = distance / speed;
    }
    public void CombineWithMaic()
    {
        var vfx = GetComponentInChildren<Animator>();
        vfx.transform.position = hit;
        effect.Apply(vfx);
    }
    private void DetectCollier()
    {
        if (timeOfArrival <= 0)
        {
            //por algum motivo que eu n sei se eu colocar o fired = false embaixo dessas duas funções
            //o fired não é setado imediatamente com false, o que ocasiona em erro por chamar as funções 
            //mais de uma vez;
            fired = false;
            CombineWithMaic();
            CalculateDamage();
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
            EndAniamtion();
        }
    }
    private void EndAniamtion()
    {
        if (transform == null)
        {
            return;
        }
        transform.position = hit;
        Animator ani = GetComponentInChildren<Animator>();

        if (ani.GetCurrentAnimatorStateInfo(0).IsName(animationName) && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            Timeout();
        }
    }
    public void CalculateDamage()
    {
        if (target)
        {
            Debug.Log(target);
            var playerProperty = target.GetComponent<PlayerProperty>();
            if (playerProperty)
                playerProperty.life -= damage;
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
