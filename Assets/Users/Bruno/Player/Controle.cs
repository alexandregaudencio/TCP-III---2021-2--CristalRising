using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Controle : MonoBehaviourPun
{
    private new Transform transform;
    public float speed;
    public Weapon gun;
    public Spells spell;
    public Animator animator;
    private GameObject aux;
    void Start()
    {
        this.transform = GetComponent<Transform>();
        if (aux)
            aux = Instantiate(animator.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.Use();
            //if (photonView.IsMine)
            //    gun.GetComponent<PhotonView>().RPC("Use", RpcTarget.All);
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            gun.Reload();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (spell)
            {
                (spell as IActive).Aim();
                (spell as IEffect).Apply(aux.GetComponent<Animator>());
            }
        }
    }
}
