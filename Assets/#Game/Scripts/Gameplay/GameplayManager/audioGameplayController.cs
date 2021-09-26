using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

public class audioGameplayController : MonoBehaviourPunCallbacks
{

    [SerializeField] AudioSource gameplayScene;
    [SerializeField] AudioSource secondsRemaning;
    [SerializeField] AudioSource fireAudio;
    [SerializeField] AudioSource firstBloodSource;
    [SerializeField] AudioSource startGameVoice;
    [SerializeField] AudioSource fireSource;
    [SerializeField] AudioSource[] tiroPlaced;
    [SerializeField] AudioSource[] audioCircleArea;
    
    private AudioClip voiceLines;
    private AudioClip shootAudio;
    private AudioClip firstBloodClip;
    public PhotonView PV;
    public static audioGameplayController instance;
    private int gameplaySceneTimeSamples;
    private int secondsRemaningTimeSamples;
    private int fireTimeSamples;
    private int firstbloodTimeSamples;
    private bool secondsRemaningTrue;
    private bool gameplaySceneTrue;
    public bool dominouBlue=true;
    public bool dominouRed=true;
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

        //int indexPlayer = (int)PhotonNetwork.LocalPlayer.CustomProperties["indexPlayer"];
        //string pTeam = PhotonNetwork.LocalPlayer.NickName;
        //Debug.Log("nome :"  + pTeam);
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
    public void audioCharacterScenePVMine(int identificador/*, string id*/)
    {
        //if(PV.IsMine)
        //int indexPlayer = (int)PhotonNetwork.LocalPlayer.CustomProperties["indexPlayer"];
        //Debug.Log("id : " + id);
        string name = PhotonNetwork.LocalPlayer.NickName;
        /*if (name == id) */tiroPlaced[identificador].Play();
        //Debug.Log("id : " + id);
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
        if (nameVoice == "startGame")
        {
            startGameVoice.clip = RoomConfigs.instance.charactersOrdered[1].gameStarted;
            //= voiceLines;
            startGameVoice.Play();
       }
    }
    //tiro
    public void audioPlayerFire(string audioName, AudioSource audioSource)
    {   
        //if(nameVoice == ) ....
        int characterIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["characterIndex"];
        shootAudio = RoomConfigs.instance.charactersOrdered[characterIndex].shoot;

        audioSource.clip = shootAudio;
        audioSource.Play();
        fireTimeSamples = audioSource.timeSamples;
        PV.RPC("SendAudioPlayer", RpcTarget.Others, audioName, fireTimeSamples);
    }

    public void audioFirstBlood(string nameVoice, int id)
    {
        firstBloodClip = RoomConfigs.instance.charactersOrdered[1].firstBlood;
        firstBloodSource.clip = firstBloodClip;
        firstBloodSource.Play();
        firstbloodTimeSamples = firstBloodSource.timeSamples;
        PV.RPC("SendAudioPlayer", RpcTarget.Others, nameVoice, firstbloodTimeSamples, firstBloodClip);
    
    }
    public void audioAreaRed()
    {
        string pTeam = PhotonNetwork.LocalPlayer.GetPhotonTeam().Name;
        if (pTeam == "Blue")
        {
            audioCircleArea[0].Play();
        }
        if (pTeam == "Red")
        {
            audioCircleArea[1].Play();
           
        }
        dominouRed = false;
    }
    public void audioAreaBlue()
    {
        string pTeam = PhotonNetwork.LocalPlayer.GetPhotonTeam().Name;
        if (pTeam == "Blue")
        {
            audioCircleArea[1].Play();
            
        }
        if (pTeam == "Red")
        {
            audioCircleArea[0].Play();
        }
        dominouBlue = false;
    }

    [PunRPC]
    private void SendAudioPlayer(string audioName, int timeSample)
    {
        if (audioName == "shoot")
        {
          fireAudio.Play();
          fireAudio.timeSamples = timeSample;


        }
        if (audioName == "firstBlood")
        {
            //firstBloodSource.clip = clip;
            firstBloodSource.Play();
            firstBloodSource.timeSamples = timeSample;

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


    [PunRPC]
    private void PlayFirstBloodAudio()
    {
        int index = (int)PhotonNetwork.LocalPlayer.CustomProperties["characterIndex"];
        fireAudio.clip = RoomConfigs.instance.charactersOrdered[index].firstBlood;
        fireAudio.Play();
    }






}
