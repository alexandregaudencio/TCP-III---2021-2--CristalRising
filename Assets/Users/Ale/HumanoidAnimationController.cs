using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class HumanoidAnimationController : MonoBehaviourPunCallbacks
{
    private Animator animator;
    [SerializeField] private Transform normalCam;
    [SerializeField] private Transform spine;
    [SerializeField] private Transform neck;
    [SerializeField] private Controle controle;
    PlayerController playerController;
    private Weapon Weapon;
    private float aux;

    PhotonView PV;

    [SerializeField] private new Rigidbody rigidbody;
    void Start()
    {

        animator = GetComponent<Animator>();
        controle = GetComponentInParent<Controle>();
        PV = GetComponent<PhotonView>();
        playerController = GetComponentInParent<PlayerController>();
        rigidbody = GetComponentInParent<Rigidbody>();
        Weapon = GetComponentInChildren<Weapon>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (PV.IsMine)
        {
            ProcessRunAnimation();
            ProcessAimTransform();
            ProcessReloading();
            ProcessShooting();
            ProcessHabiliityOne();
            ProcessHabiliityTwo();
            ProcessJump();
        }

    }

    private void ProcessHabiliityOne()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !animator.GetBool("Reloading"))
        {
            animator.SetTrigger("Habillity_1");
        }
    }
    private void ProcessHabiliityTwo()
    {
        if (Input.GetKeyDown(KeyCode.E) && !animator.GetBool("Reloading"))
        {
            animator.SetTrigger("Habillity_2");
        }
    }
    private void ProcessShooting()
    {
        if (Input.GetMouseButton(0) && !animator.GetBool("Reloading"))
        {
            animator.SetTrigger("Shoot");
        }

    }
    private void ProcessRunAnimation()
    {
        animator.SetFloat("Vertical", Input.GetAxis("Vertical"));
        animator.SetFloat("Horizontal", Input.GetAxis("Horizontal"));

    }
    private void ProcessAimTransform()
    {
        spine.rotation = normalCam.rotation;
        //spine.rotation = Quaternion.EulerAngles(normalCam.rotation.x, normalCam.rotation.y,normalCam.rotation.z);
    }

    public void ProcessReloading()
    {
        //bool isReloading = controle.gun.recarregando;
        //if (Input.GetKeyDown(KeyCode.R) && !Weapon)
        //{
        //    animator.SetBool("Reloading", true);
        //    StartCoroutine(DisablingReloading());
        //}
        if (Input.GetKeyDown(KeyCode.R))
            animator.SetTrigger("Reloading");
    }


    //TODO
    IEnumerator DisablingReloading()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Reloading", false);
    }

    void ProcessJump()
    {
        animator.SetBool("OnFloor", playerController.GroundCheck);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Jumping");
        }
        if (!playerController.GroundCheck)
        {
            aux = Mathf.Lerp(aux, rigidbody.velocity.normalized.y, 0.05f);
        }
        else
        {
            aux = 0;
        }
        animator.SetFloat("JumpDir", aux);
    }



    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == PhotonNetwork.LocalPlayer)
        {
            animator.SetBool("Death", (bool)targetPlayer.CustomProperties["isDead"]);
        }
    }


}
