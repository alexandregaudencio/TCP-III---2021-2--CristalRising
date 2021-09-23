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
    private AudioClip fired;
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
        PV.RPC("SendAudioPlayer", RpcTarget.Others, nameVoice, fireTimeSamples, fired);
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
    private void SendAudioPlayer(string audio, int timeSample, AudioClip clip)
    {
        if (audio == "fire")
        {
            fireAudio.Play();
            fireAudio.timeSamples = timeSample;

        }
        if (audio == "firstBlood")
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
    private void PlayAudioToAll()
    {
        int index = (int)PhotonNetwork.LocalPlayer.CustomProperties["characterIndex"];
        fireAudio.clip = RoomConfigs.instance.charactersOrdered[index].firstBlood;
        fireAudio.Play();
        Debug.Log("Toca som de first blood");
    }






}
