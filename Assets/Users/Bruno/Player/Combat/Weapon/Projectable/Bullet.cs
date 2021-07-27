using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float existenceTomeout;
    private float countTime;
    public GameObject body;
    public IEffect effect;
    [HideInInspector]
    public Pool pool;
    private float timeOfArrival;
    private float distance;
    private new Transform transform;
    private bool fired;
    public RaycastHit hit;
    public void Inicialize()
    {
        countTime = 0;
        this.transform = gameObject.transform;
        fired = true;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        this.gameObject.SetActive(true);
        GetComponentInChildren<MeshRenderer>().gameObject.SetActive(true);
    }
    void Update()
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
            else
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

            transform.position += transform.forward * speed * Time.deltaTime;
        }
        else
        {
            transform.position = hit.point;
            if (GetComponentInChildren<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Explosion"))
            {
                Timeout();

            }
        }
    }
    public void Timeout()
    {
        countTime = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
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
        effect.Apply(transform);
    }
    public void CalculateDamage()
    {
    }
    public void Inject(GameObject effect)
    {
        this.effect = effect.GetComponent<IEffect>();
    }
}
