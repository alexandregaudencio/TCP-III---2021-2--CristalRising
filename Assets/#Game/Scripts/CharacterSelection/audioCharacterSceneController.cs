using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class audioCharacterSceneController :  MonoBehaviourPunCallbacks
{
    
    [SerializeField] AudioSource characterScene;
    [SerializeField] AudioSource selectCharacter;
    [SerializeField] AudioSource[] buttonChooseCharacter;
    public PhotonView PV;
    public static audioCharacterSceneController instance;
    public int characterSceneTimeSamples;
    private bool characterSceneTrue;
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
    public void audioCharacterScenePVMine(string nameAudio)
    {

        if (nameAudio.Equals("selectCharacter"))
        {
            selectCharacter.Play();
        }
        if (nameAudio[0].Equals('b'))
        {
            string[] infoCharacter = nameAudio.Split('.');
            int numCharacter;
            int.TryParse(infoCharacter[1], out numCharacter);
            buttonChooseCharacter[numCharacter].Play();
        }
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
