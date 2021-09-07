using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet : MonoBehaviourPun, Damage
{
    public float speed;
    public int damage;
    public int criticalDamage;
    public float existenceTomeout;
    private float countTime;
    private Color color;
    public IEffect effect;
    [HideInInspector]
    public Pool pool;
    private float timeOfArrival;
    private float distance;
    private new Transform transform;
    private bool fired = false;
    [HideInInspector]
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
    public void Inicialize(Vector3 point, float timeOfArrival, Vector3 pos, Vector3 rot, int targetId/*, Vector3 color*/)
    {
        //this.color = new Color(color.x, color.y, color.z,1);
        //GetComponentInChildren<Renderer>().material.SetColor("_Color", this.color);
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
        //vfx.gameObject. GetComponent<Renderer>().material.SetColor("_Color", color);
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
            if (target.GetComponent<Collider>().bounds.Contains(transform.position))
            {
                fired = false;
                CombineWithMaic();
                Invoke("CalculateDamage",0.1f);
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
            var chunks = target.GetComponentsInChildren<ChunkDetector>();
            var playerProperty = target.GetComponent<PlayerProperty>();
            Player pTarget = target.GetPhotonView().Controller;
            if (playerProperty)
            {
                int value = 0;
                foreach (var c in chunks)
                {
                    var result = c.DetectHit(GetComponent<Collider>());

                    if (result != null)
                    {
                        if (result.Equals(ChunkDetector.head))
                        {
                            value = criticalDamage;
                        }
                        else if (result.Equals(ChunkDetector.body))
                        {
                            value = this.damage;
                        }
                    }
                }

                playerProperty.Life = value;
            }
        }
    }

    public GameObject GetTarget()
    {
        return target;
    }
}
