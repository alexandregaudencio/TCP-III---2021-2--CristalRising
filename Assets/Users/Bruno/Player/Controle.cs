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
    //public Animator animator;
    private GameObject aux;
    private Animator playerAnim;
    void Start()
    {
        this.transform = GetComponent<Transform>();
        //if (aux)
            //aux = Instantiate(animator.gameObject);
        //playerAnim = GetComponentInChildren<Animator>();
        //playerAnim.speed = 10;
    }

    // Update is called once per frame
    void Update()
    {
        (spell as IActive).Aim();
        if (!photonView.IsMine)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            //playerAnim.SetTrigger("attack");
            gun.Use();

            if (spell)
            {
                spell.Use();
            }
            //if (photonView.IsMine)
            //    gun.GetComponent<PhotonView>().RPC("Use", RpcTarget.All);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.Reload();
        }
    }
}
