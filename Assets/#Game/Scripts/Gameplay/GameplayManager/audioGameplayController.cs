using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class audioGameplayController : MonoBehaviourPunCallbacks
{

    [SerializeField] AudioSource gameplayScene;
    [SerializeField] AudioSource secondsRemaning;
    [SerializeField] AudioSource fireAudio;
    [SerializeField] AudioSource startGameVoice;
    [SerializeField] AudioSource fireSource;
    [SerializeField] AudioSource[] tiroPlaced;

    
    private AudioClip voiceLines;
    private AudioClip fired;
    public PhotonView PV;
    public static audioGameplayController instance;
    private int gameplaySceneTimeSamples;
    private int secondsRemaningTimeSamples;
    private int fireTimeSamples;
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
   
    //masterclient manda pra todos
    public void audioGameplayScenePV(string nameAudio)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (nameAudio == "gameplayScene")
            {
                gameplayScene.Play();

            }
            if (nameAudio == "secondsRemaning")
            {
                secondsRemaning.Play();

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
    //local
    public void audioCharacterScenePVMine(int identificador)
    {
        if(PV.IsMine)tiroPlaced[identificador].Play();
        
    }
        //manda o timesample
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
    //falas
    public void audioPlayerVoiceLines(string nameVoice, int id)
    {
        if (nameVoice == "startGame") voiceLines = RoomConfigs.instance.charactersOrdered[id].gameStarted;
        startGameVoice.clip = voiceLines;
        startGameVoice.Play();
    }
    //tiro
    public void audioPlayerFire(string nameVoice, int id)
    {
         fired = RoomConfigs.instance.charactersOrdered[id].fired;
        fireSource.clip = fired;
        fireSource.Play();
        fireTimeSamples = fireAudio.timeSamples;
        PV.RPC("SendAudioPlayer", RpcTarget.Others, nameVoice, fireTimeSamples);
    }

    [PunRPC]
    private void SendAudioPlayer(string audio, int timeSample)
    {
        if (audio == "fire")
        {
            fireAudio.Play();
            fireAudio.timeSamples = timeSample;

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
