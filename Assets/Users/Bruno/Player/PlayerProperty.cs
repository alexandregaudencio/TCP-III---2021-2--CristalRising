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
    public GameObject[] SpawnPointsTimeAzul;
    public GameObject[] SpawnPointTimeVermelho;
    public Slider lifeUi;

    private float moveSpeed;
    private float maxJumpHeight;
    public ShowDamage sd;

    private float buffereMoveSpeed;
    private float bufferMaxJumpHight;
    public float life;
    public float Life
    {
        get { return lifeUi.value; }
        set
        {
            lifeUi.value -= value;
            sd.Value = value.ToString();
        }
    }

    private void Start()
    {
        moveSpeed = GetComponent<PlayerController>().moveSpeed;
        bufferMaxJumpHight = GetComponent<PlayerController>().jumpForce;

        buffereMoveSpeed = moveSpeed;
        bufferMaxJumpHight = maxJumpHeight;

        if (GetComponent<PhotonView>().IsMine)
        {
            lifeUi.maxValue = life;
            lifeUi.value = life;
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

        lifeUi.value = life;
    }
}

