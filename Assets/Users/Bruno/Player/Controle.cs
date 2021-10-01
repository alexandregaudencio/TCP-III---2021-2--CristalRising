using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Controle : MonoBehaviourPun
{
    private new Transform transform;
    public float speed;
    public Weapon gun;
    public Spells spell;
    public Spells spellHabillityQ;
    public Spells spellHabillityE;
    public Spells spellHabillityF;
    public Spells spellHabillityRightMouse;

    //public Animator animator;
    private GameObject aux;
    private Animator playerAnim;

    [SerializeField] private GameObject ammoText;

    public GameObject AmmoText { get => ammoText; set => ammoText = value; }

    void Start()
    {
        this.transform = GetComponent<Transform>();
        //if (aux)
        //    aux = Instantiate(animator.gameObject);
        playerAnim = GetComponentInChildren<Animator>();
        playerAnim.speed = 10;

        ammoText = GameObject.Find("AmmoText");
        UpdateAmmoText();
    }

    // Update is called once per frame
    void Update()
    {
        (spell as IActive).Aim();
        if (!photonView.IsMine)
        {
            return;
        }
        if (gun.Ammo > 0 && gun.recarregando == false)
        {


            if (Input.GetMouseButtonDown(0))
            {
                //playerAnim.SetTrigger("attack");
                gun.Use();

                UpdateAmmoText();
                if (spell)
                {
                    spell.Use();
                }
                //if (photonView.IsMine)
                //    gun.GetComponent<PhotonView>().RPC("Use", RpcTarget.All);
            }

        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            gun.recarregando = true;
        }


    }

    public void UpdateAmmoText()
    {
        ammoText.GetComponent<TMP_Text>().text = gun.Ammo.ToString() + "/" + gun.MaxAmmo.ToString();
    }
}
