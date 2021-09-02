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
    public GameObject[] SpawnPointsTimeAzul;
    public GameObject[] SpawnPointTimeVermelho;
    public float life;

    private float moveSpeed;
    private float maxJumpHeight;
    public ShowDamage sd;

    public float Life
    {
        get { return this.life; }
        set
        {
            this.life -= value;
            sd.Value = value.ToString();
        }
    }

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
        if (GetComponent<PhotonView>().IsMine)
        {
            textUiLife.SetActive(true);
        }
    }
    private void Update()
    {
        var tmp = textUiLife.GetComponent<Text>();
        if (tmp)
        {
            tmp.text = life.ToString();
        }
    }

    internal void SetAttribute(Attribute attibut)
    {
        GetComponent<PlayerController>().moveSpeed = attibut.speed;
        GetComponent<PlayerController>().jumpForce = attibut.strenght;
    }

    public void ResetProperty()
    {
        GetComponent<PlayerController>().moveSpeed = buffereMoveSpeed;
        GetComponent<PlayerController>().jumpForce = buffereMoveSpeed;

        this.life = bufferlife;
    }
}

