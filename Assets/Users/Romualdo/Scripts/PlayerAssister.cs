using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerAssister : MonoBehaviour
{
    public byte MyTeam;
    public int index;
    public PhotonView PV;
    public static PlayerAssister T2;
    public GameObject NI;
    public  bool InRoom;
    public GameObject[] PlayerList;
    public bool PodeIns;
    public byte jogadorEscolhido;
    void Start()
    {
        jogadorEscolhido = 50;
        PV = GetComponent<PhotonView>();
        
        if(PV.IsMine)
        {
            Debug.Log("teste is mine");
            T2 = this;
           
            PV.RPC("RPC_GetTeam", RpcTarget.MasterClient);
        }
        DontDestroyOnLoad(this.gameObject);
        
    }

    public void CallGetTeam()
    {
        if (PV.IsMine)
            PV.RPC("RPC_GetTeam", RpcTarget.All);
    }

    void Update()
    {

        index = SceneManager.GetActiveScene().buildIndex;
        
        
        
        if(index==3 && PodeIns == true)
        {
            if (index == 3 && PodeIns == true)
            {

                if (PV.IsMine)
                {
                    if (MyTeam == 2)
                    {
                        Debug.Log("time veremlho");

                    }
                }
               
            }

            if (PV.IsMine)
            {
                if (MyTeam == 1)
                {
                    Debug.Log("TimeAzul");
                   
                    
                }
            } 
       
        }
       




    }

    public void Choose1()
    {
        jogadorEscolhido = 0;
       // if(MyTeam==1 && InRoom ==true)
       // {
          
            //if(PV.IsMine)
            //{
           //     PhotonNetwork.Instantiate(PlayerList[0].name, this.SpawnersTimeAzul[spawnpicker].transform.position, this.SpawnersTimeAzul[spawnpicker].transform.rotation);
           // }
        //}
       // else
        //{
         //   int spawnpicker = Random.Range(0, this.SpawnersTimeAzul.Length);
           // if (PV.IsMine)
          //  {
            //    PhotonNetwork.Instantiate(PlayerList[0].name, this.SpawnersTimeAzul[spawnpicker].transform.position, this.SpawnersTimeAzul[spawnpicker].transform.rotation);
           // }
        
    }

    public void Choose2()
    {
        jogadorEscolhido = 1;
    }



    public void Choose3()
    {
        jogadorEscolhido =2;
    }



    [PunRPC]
    void RPC_GetTeam()
    {
        MyTeam = LobbyController.instance.NextPlayerTeam;
        PV.RPC("RPC_SentTeam", RpcTarget.OthersBuffered, MyTeam);
    }

    [PunRPC]
    void RPC_SentTeam(byte whichTeam)
    {
        MyTeam = whichTeam;
    }
}

