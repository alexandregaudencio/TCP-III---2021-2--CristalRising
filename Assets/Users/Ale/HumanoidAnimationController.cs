using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Animator))]
public class HumanoidAnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private Transform normalCam;
    [SerializeField] private Transform spine;
    [SerializeField] private Transform neck;
    [SerializeField] private Controle controle;
    PlayerController playerController;
    private Weapon Weapon;


    PhotonView PV;

    [SerializeField] private new Rigidbody rigidbody;
    void Start()
    {
        
        animator = GetComponent<Animator>();
        controle = GetComponentInParent<Controle>();
        PV = GetComponent<PhotonView>();
        playerController = GetComponentInParent<PlayerController>();
        Weapon = GetComponentInChildren<Weapon>();

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(PV.IsMine)
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
        animator.SetTrigger("Habillity_1");
    }
    private void ProcessHabiliityTwo()
    {
        animator.SetTrigger("Habillity_2");
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

    }


    //TODO
    IEnumerator DisablingReloading()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Reloading", false);
    }
   
    void ProcessJump()
    {
        animator.SetBool("Jumping", !playerController.GroundCheck);
        //if(Input.GetKeyDown(KeyCode.Space) && playerController.GroundCheck)
        //{
             
        //}
   
        
    }



}
