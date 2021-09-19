using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class audioGameplayController : MonoBehaviourPunCallbacks
{

    [SerializeField] AudioSource gameplayScene;
    [SerializeField] AudioSource secondsRemaning;
    [SerializeField] AudioSource[] voiceLineCharacter;
    public PhotonView PV;
    public static audioGameplayController instance;
    public int gameplaySceneTimeSamples;
    public int secondsRemaningTimeSamples;
    private bool secondsRemaningTrue;
    private bool gameplaySceneTrue;
    public void Start()
    {
        gameplaySceneTrue = false;
        secondsRemaningTrue = false;
        //characterScene = GetComponent<AudioSource>();
        //PV = GetComponent<PhotonView>();
        instance = this;
        //characterSceneTimeSamples = 0;

    }
    private void FixedUpdate()
    {
       if(gameplaySceneTrue) sendImeSample("gameplayScene");
        if (secondsRemaningTrue) sendImeSample("secondsRemaning");
    }
    public void audioGameplayScenePV(string nameAudio)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (nameAudio == "gameplayScene")
            {
                gameplayScene.Play();
               // gameplaySceneTrue = true;

            }
            if (nameAudio == "secondsRemaning")
            {
                secondsRemaning.Play();
               // secondsRemaningTrue = true;

            }
        }
       
         if (!PhotonNetwork.IsMasterClient)
        {

            if (nameAudio == "gameplayScene")
            {
                gameplaySceneTrue = true;
                PV.RPC("SendTrue", RpcTarget.MasterClient, "gameplayScene", gameplaySceneTrue);
            }

            if (nameAudio == "secondsRemaning")
            {
                secondsRemaningTrue = true;
                PV.RPC("SendTrue", RpcTarget.MasterClient, "secondsRemaning", secondsRemaningTrue);
            }

        }
       
    }
    public void sendImeSample(string nameAudio)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (nameAudio == "gameplayScene")
            {
                gameplaySceneTimeSamples = gameplayScene.timeSamples;
                PV.RPC("SendAudio", RpcTarget.Others, "gameplayScene", gameplaySceneTimeSamples);
                gameplaySceneTrue = false;
            }
            if (nameAudio == "secondsRemaning")
            {
                secondsRemaningTimeSamples = secondsRemaning.timeSamples;
                PV.RPC("SendAudio", RpcTarget.Others, "secondsRemaning", secondsRemaningTimeSamples);
                secondsRemaningTrue = false;
            }
        }
    }
    public  void audioGameplayPVMine(string nameAudio)
    {

       
        if (nameAudio[0].Equals('v'))
        {
            string[] infoCharacter = nameAudio.Split('.');
            int numCharacter;
            int.TryParse(infoCharacter[1], out numCharacter);
            voiceLineCharacter[numCharacter].Play();


        }
    }


    [PunRPC]
    private void SendAudio(string audio, int timeSample)
    {
        if (audio == "gameplayScene")
        {
            gameplayScene.Play();
            gameplayScene.timeSamples = timeSample;
            
        }
        if (audio == "secondsRemaning")
        {
            secondsRemaning.Play();
            secondsRemaning.timeSamples = timeSample;
            
        }


    }

    [PunRPC]
    private void SendTrue( string name,bool istrue)
    {
        if(name== "gameplayScene" )gameplaySceneTrue = istrue;
        if(name== "secondsRemaning") secondsRemaningTrue = istrue;


    }
}
