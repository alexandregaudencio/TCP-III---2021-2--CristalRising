using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerController))]
public class PlayerProperty : MonoBehaviour
{

    private float moveSpeed;
    private float maxJumpHeight;
    public ShowDamage sd;

    private float buffereMoveSpeed;
    private float bufferMaxJumpHight;

    private ExitGames.Client.Photon.Hashtable HashProperty = new ExitGames.Client.Photon.Hashtable();

    private int life;
    public int Life
    {
        get { return (int)GetComponent<PhotonView>().Controller.CustomProperties["HP"]; }
        set
        {
            int hp = (int)GetComponent<PhotonView>().Controller.CustomProperties["HP"];
            HashProperty["HP"] = hp - value;
            GetComponent<PhotonView>().Controller.SetCustomProperties(HashProperty);

            sd.Value = value.ToString();
        }
    }


    private void Start()
    {
        moveSpeed = GetComponent<PlayerController>().moveSpeed;
        bufferMaxJumpHight = GetComponent<PlayerController>().jumpForce;

        buffereMoveSpeed = moveSpeed;
        bufferMaxJumpHight = maxJumpHeight;
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

        //lifeUi.value = life;
    }
}

