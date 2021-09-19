using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class audioCharacterSceneController :  MonoBehaviourPunCallbacks
{
    
    [SerializeField] AudioSource characterScene;
    [SerializeField] AudioSource selectCharacter;
    [SerializeField] AudioSource buttonCharacterAudio;
    private AudioClip voiceLines;
    public PhotonView PV;
    public static audioCharacterSceneController instance;
    public int characterSceneTimeSamples;
    public bool characterSceneTrue;
    public void Start()
    {
        //characterScene = GetComponent<AudioSource>();
        //PV = GetComponent<PhotonView>();
        characterSceneTrue = false;
        instance = this;
        //characterSceneTimeSamples = 0;
       
    }
    private void FixedUpdate()
    {
        if(characterSceneTrue)sendImeSample("characterScene");
    }
    //masterclient manda pra todos
    public void audioCharacterScenePV(string nameAudio)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (nameAudio == "characterScene")
            {
                characterScene.Play();
               
            }
        }
        if (!PhotonNetwork.IsMasterClient)
        {

            if (nameAudio == "characterScene")
            {
                characterSceneTrue = true;
                PV.RPC("SendTrue", RpcTarget.MasterClient, "characterScene", characterSceneTrue);
            }

        }
    }
    //manda o timesample
    public void sendImeSample(string nameAudio)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (nameAudio == "characterScene")
            {
                 characterSceneTimeSamples = characterScene.timeSamples;
                PV.RPC("SendAudio", RpcTarget.Others, "characterScene", characterSceneTimeSamples);
                characterSceneTrue = false;
            }
        }
    }
    //audio local
    public void audioCharacterScenePVMine(string nameAudio)
    {

        if (nameAudio.Equals("selectCharacter"))
        {
            selectCharacter.Play();
        }
       
    }
    //falas
    public void audioPlayerVoiceLines(string nameVoice, int id)
    {
        if(nameVoice== "characterSelected") voiceLines = RoomConfigs.instance.charactersOrdered[id].selectCharacter;
        buttonCharacterAudio.clip = voiceLines;
        buttonCharacterAudio.Play();
    }

   [PunRPC]
    private void SendAudio(string audio, int timeSample)
    {
        if (audio == "characterScene")
        {
            characterScene.Play();
            characterScene.timeSamples = timeSample;
        }


    }

    [PunRPC]
    private void SendTrue(string name, bool istrue)
    {
        if (name == "characterScene") characterSceneTrue = istrue;
    }
}
