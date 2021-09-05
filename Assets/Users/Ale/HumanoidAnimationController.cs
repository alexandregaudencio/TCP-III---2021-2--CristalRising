using Photon.Pun;
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

    PhotonView PV;
    void Start()
    {
        animator = GetComponent<Animator>();
        controle = GetComponentInParent<Controle>();
        PV = GetComponent<PhotonView>();
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

    private void ProcessReloading()
    {
        //bool isReloading = controle.gun.recarregando;
        if (Input.GetKeyDown(KeyCode.Q) && !animator.GetBool("Reloading"))
        {
            animator.SetBool("Reloading", true);
            StartCoroutine(DisablingReloading());
        }
    }


    //TODO
    IEnumerator DisablingReloading()
    {
        yield return new WaitForSeconds(1.5f);
        animator.SetBool("Reloading", false);
    }
   

}
