using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerProperty : MonoBehaviour
{
    public GameObject textUiLife;

    public float life;
    private float moveSpeed;
    private float maxJumpHeight;

    private float buffereMoveSpeed;
    private float bufferMaxJumpHight;
    private float bufferlife;
    private void Start()
    {
        moveSpeed = GetComponent<PlayerController>().moveSpeed;
        bufferMaxJumpHight = GetComponent<PlayerController>().jumpForce;

        buffereMoveSpeed = moveSpeed;
        bufferMaxJumpHight = maxJumpHeight;

        bufferlife = life;
        if (GetComponent<PhotonView>().IsMine) {
            textUiLife.SetActive(true);
        }
    }
    private void Update()
    {
        Debug.Log(textUiLife.GetComponent<Text>());
        var tmp = textUiLife.GetComponent<Text>();
        if (tmp)
            tmp.text = life.ToString();
        if (life < 0)
            dead();
    }

    internal void SetAttribute(Attribute attibut)
    {
        GetComponent<PlayerController>().moveSpeed = attibut.speed;
        GetComponent<PlayerController>().jumpForce = attibut.strenght;
    }

    public void Nomalize()
    {
        GetComponent<PlayerController>().moveSpeed = buffereMoveSpeed;
        GetComponent<PlayerController>().jumpForce = buffereMoveSpeed;

        life = bufferlife;
    }
    private void dead()
    {
        Debug.Log("you died!");
        throw new NotImplementedException();
    }
}
