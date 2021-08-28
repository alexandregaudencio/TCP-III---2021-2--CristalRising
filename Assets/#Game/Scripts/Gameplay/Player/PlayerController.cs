using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class PlayerController : MonoBehaviour
{
    [SerializeField] [Range(1, 50)] public float moveSpeed;
    [SerializeField] [Range(1, 3)] private int sprintModifier;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] public float jumpForce;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float fallMultiplier;
    public GameObject teamIdentify;
    public Animator animator;

    private Vector3 dir;
    private Rigidbody playerRb;
    [SerializeField] private Camera normalCam;

    private float maxRotationY;
    private float rotationX;
    private float baseFOV;
    private float sprintFOVModifier = 1.5f;

    public Text debug;
    private bool jump, groundCheck;

    PhotonView PV;
    void Start()
    {
        if (!PV.IsMine)
            Destroy(normalCam);
        else
        {
            baseFOV = normalCam.fieldOfView;
        }
        playerRb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        animator.speed = 10;

        string team = PhotonNetwork.LocalPlayer.GetPhotonTeam().Name;

        if (team == "Red")
        {
            Debug.Log("Red");
            teamIdentify.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else
        {
            Debug.Log("Blue");
            teamIdentify.GetComponent<Renderer>().material.SetColor("_Color", Color.blue);
        }

    }
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (!PV.IsMine)
            return;
        CameraRotation();
        Jumping();

        // if (transform.position.y - chao.transform.position.y > 0.1) {
        //     debug.text = "Not Grounded";
        // } else {
        //     debug.text = "Grounded";
        //     groundCheck = true;
        // }

        // jump = Input.GetKeyDown (KeyCode.Space);
        // if (jump && groundCheck) {
        //     // groundCheck = false;
        //     playerRb.AddForce (0, jumpForce, 0, ForceMode.Impulse);
        // }

        // if (playerRb.velocity.y < 0 && playerRb.velocity.y > maxFallSpeed) {
        //     playerRb.velocity += new Vector3 (0, Physics.gravity.y * fallMultiplier * Time.deltaTime, 0);
        // }

    }

    private void FixedUpdate()
    {
        if (!PV.IsMine)
            return;
        Sprinting();

        // float horAxis = Input.GetAxisRaw ("Horizontal");
        // float verAxis = Input.GetAxisRaw ("Vertical");

        // bool sprint = Input.GetKey (KeyCode.LeftShift) && groundCheck;
        // bool isSprinting = sprint && verAxis > 0;

        // float adjustedSpeed = moveSpeed;
        // if (isSprinting) adjustedSpeed *= sprintModifier;

        // // Controla o campo de visão se o jogador estiver correndo
        // if (isSprinting) {
        //     normalCam.fieldOfView = Mathf.Lerp (normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 5f);
        // } else {
        //     normalCam.fieldOfView = Mathf.Lerp (normalCam.fieldOfView, baseFOV, Time.deltaTime * 5f);
        // }

        // // Move o jogador na direção que está virado
        // dir = player.TransformVector (new Vector3 (horAxis, 0, verAxis).normalized);
        // playerRb.MovePosition (playerRb.position + dir * adjustedSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("chao"))
        {
            animator.SetBool("onFloor", true);
            groundCheck = true;
            debug.text = "Grounded";
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("chao"))
        {
            groundCheck = false;
            debug.text = "Not Grounded";
            animator.SetBool("onFloor", false);
        }
    }

    void CameraRotation()
    {
        // Declaração da rotação da câmera e angulação mínima e máxima.
        rotationX = Mathf.Lerp(rotationX, Input.GetAxisRaw("Mouse X") * 2, 100 * Time.deltaTime);
        maxRotationY = Mathf.Clamp(maxRotationY - (Input.GetAxisRaw("Mouse Y") * 2 * 100 * Time.deltaTime), -30, 30);
        // Rotação da câmera através do mouse.
        transform.Rotate(0, rotationX, 0, Space.World);
        normalCam.transform.rotation = Quaternion.Lerp(normalCam.transform.rotation, Quaternion.Euler(maxRotationY * 2, transform.eulerAngles.y, 0), 100 * Time.deltaTime);

        // Posição da câmera acompanha a posição do jogador.
        normalCam.transform.position = Vector3.Lerp(normalCam.transform.position, normalCam.transform.position, 50 * Time.deltaTime);
    }

    private void Jumping()
    {
        jump = Input.GetKeyDown(KeyCode.Space);
        if (jump && groundCheck)
        {
            // groundCheck = false;
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("jump");
        }

        if (playerRb.velocity.y < 0 && playerRb.velocity.y > maxFallSpeed)
        {
            playerRb.velocity += new Vector3(0, Physics.gravity.y * fallMultiplier * Time.deltaTime, 0);
        }
    }

    private void Sprinting()
    {
        float horAxis = Input.GetAxisRaw("Horizontal");
        float verAxis = Input.GetAxisRaw("Vertical");

        bool sprint = Input.GetKey(KeyCode.LeftShift) && groundCheck;
        bool isSprinting = sprint && verAxis > 0;

        float adjustedSpeed = moveSpeed;
        if (isSprinting) adjustedSpeed *= sprintModifier;
        if (horAxis > 0)
        {
            animator.SetTrigger("right");
        }
        if (horAxis < 0)
        {
            animator.SetTrigger("left");
        }
        if (verAxis > 0)
        {
            animator.SetTrigger("forward");
        }
        if (verAxis < 0)
        {
            animator.SetTrigger("back");
        }
        // Controla o campo de visão se o jogador estiver correndo
        if (isSprinting)
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 5f);
        }
        else
        {
            normalCam.fieldOfView = Mathf.Lerp(normalCam.fieldOfView, baseFOV, Time.deltaTime * 5f);
        }

        // Move o jogador na direção que está virado
        dir = transform.TransformVector(new Vector3(horAxis, 0, verAxis).normalized);
        playerRb.MovePosition(playerRb.position + dir * adjustedSpeed * Time.deltaTime);

    }

}