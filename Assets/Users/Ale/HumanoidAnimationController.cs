using Photon.Pun;
using Photon.Realtime;
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
    //[SerializeField] private Vector3 spineRotationOffset;
    //[SerializeField] private Transform neck;
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
            ProcessJump();
           
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
        //spine.rotation = Quaternion.EulerAngles(normalCam.rotation.x+spineRotationOffset.x, 
        //    normalCam.rotation.y+spineRotationOffset.y,
        //    normalCam.rotation.z + spineRotationOffset.z);
        //spine.rotation = Quaternion.EulerAngles(normalCam.rotation.x, normalCam.rotation.y,normalCam.rotation.z);
    }

    public void ProcessReloading()
    {
        bool isReloading = controle.gun.recarregando;
        if (Input.GetKeyDown(KeyCode.R) && !Weapon)
        {
            animator.SetBool("Reloading", true);
            //StartCoroutine(DisablingReloading());
        }

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


    }

    public void StopReloading()
    {
        animator.SetBool("Reloading", false);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(targetPlayer == PhotonNetwork.LocalPlayer)
        {
            animator.SetBool("Death", (bool)targetPlayer.CustomProperties["isDead"]);
        }
    }
}
