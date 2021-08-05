using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    [SerializeField] private Transform player;
    [SerializeField][Range (1, 50)] public int moveSpeed;
    [SerializeField][Range (1, 3)] private int sprintModifier;
    [SerializeField] private Transform cameraPivot;
    [SerializeField] private Transform camera;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxFallSpeed;
    [SerializeField] private float fallMultiplier;

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
    void Start () {
        baseFOV = normalCam.fieldOfView;
        playerRb = GetComponent<Rigidbody> ();
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    void Update () {
        if (!PV.IsMine)
            return;
        CameraRotation ();
        Jumping ();

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

    private void FixedUpdate () {
        if (!PV.IsMine)
            return;
        Sprinting ();

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

    private void OnCollisionEnter (Collision other) {
        if (other.gameObject.CompareTag ("chao")) {
            groundCheck = true;
            debug.text = "Grounded";
        }
    }

    private void OnCollisionExit (Collision other) {
        if (other.gameObject.CompareTag ("chao")) {
            groundCheck = false;
            debug.text = "Not Grounded";
        }
    }

    void CameraRotation () {
        // Declaração da rotação da câmera e angulação mínima e máxima.
        rotationX = Mathf.Lerp (rotationX, Input.GetAxisRaw ("Mouse X") * 2, 100 * Time.deltaTime);
        maxRotationY = Mathf.Clamp (maxRotationY - (Input.GetAxisRaw ("Mouse Y") * 2 * 100 * Time.deltaTime), -30, 30);

        // Rotação da câmera através do mouse.
        player.Rotate (0, rotationX, 0, Space.World);
        camera.rotation = Quaternion.Lerp (camera.rotation, Quaternion.Euler (maxRotationY * 2, player.eulerAngles.y, 0), 100 * Time.deltaTime);

        // Posição da câmera acompanha a posição do jogador.
        cameraPivot.position = Vector3.Lerp (cameraPivot.position, player.position, 50 * Time.deltaTime);

    }

    private void Jumping () {
        jump = Input.GetKeyDown (KeyCode.Space);
        if (jump && groundCheck) {
            // groundCheck = false;
            playerRb.AddForce (Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (playerRb.velocity.y < 0 && playerRb.velocity.y > maxFallSpeed) {
            playerRb.velocity += new Vector3 (0, Physics.gravity.y * fallMultiplier * Time.deltaTime, 0);
        }
    }

    private void Sprinting () {
        float horAxis = Input.GetAxisRaw ("Horizontal");
        float verAxis = Input.GetAxisRaw ("Vertical");

        bool sprint = Input.GetKey (KeyCode.LeftShift) && groundCheck;
        bool isSprinting = sprint && verAxis > 0;

        float adjustedSpeed = moveSpeed;
        if (isSprinting) adjustedSpeed *= sprintModifier;

        // Controla o campo de visão se o jogador estiver correndo
        if (isSprinting) {
            normalCam.fieldOfView = Mathf.Lerp (normalCam.fieldOfView, baseFOV * sprintFOVModifier, Time.deltaTime * 5f);
        } else {
            normalCam.fieldOfView = Mathf.Lerp (normalCam.fieldOfView, baseFOV, Time.deltaTime * 5f);
        }

        // Move o jogador na direção que está virado
        dir = player.TransformVector (new Vector3 (horAxis, 0, verAxis).normalized);
        playerRb.MovePosition (playerRb.position + dir * adjustedSpeed * Time.deltaTime);

    }

}