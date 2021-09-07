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

    public GameObject[] SpawnPointsTimeAzul;
    public GameObject[] SpawnPointTimeVermelho;
    //public Slider lifeUi;

    private float moveSpeed;
    private float maxJumpHeight;
    public ShowDamage sd;

    private float buffereMoveSpeed;
    private float bufferMaxJumpHight;

    private ExitGames.Client.Photon.Hashtable HashProperty = new ExitGames.Client.Photon.Hashtable();

    public int life;
    //public int Life
    //{
    //    get { return (int)PhotonNetwork.LocalPlayer..CustomProperties["HP"]; }
    //    set
    //    {
    //        int hp = (int)PhotonNetwork.LocalPlayer.CustomProperties["HP"];
    //        HashProperty["HP"] = hp - value;
            
    //        PhotonNetwork.LocalPlayer.SetCustomProperties(HashProperty);
            
    //        //Debug.Log("Dano em " + PhotonNetwork.LocalPlayer.NickName + " " + PhotonNetwork.LocalPlayer.CustomProperties["HP"]);
    //        sd.Value = value.ToString();
    //    }
    //}
    public int Life(int value, Player target)
    {
        int hp = (int)target.CustomProperties["HP"];
        HashProperty["HP"] = hp - value;
        target.SetCustomProperties(HashProperty);
       
        sd.Value = value.ToString();
        
        return (int)target.CustomProperties["HP"];

    }

    



    private void Start()
    {
        moveSpeed = GetComponent<PlayerController>().moveSpeed;
        bufferMaxJumpHight = GetComponent<PlayerController>().jumpForce;

        buffereMoveSpeed = moveSpeed;
        bufferMaxJumpHight = maxJumpHeight;

        if (GetComponent<PhotonView>().IsMine)
        {
            //lifeUi.maxValue = life;
            //lifeUi.value = life;
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

        //lifeUi.value = life;
    }
}

