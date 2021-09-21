using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Linq;
using Photon.Pun.UtilityScripts;


public class SetUpGameplay : MonoBehaviour
{
    
    private PhotonView PV;
    public int id;
    [SerializeField] private GameObject[] spawnPointsBlue, spawnPointsRed;
    public static SetUpGameplay instance;

    public GameObject[] SpawnPointsBlue { get => spawnPointsBlue;}
    public GameObject[] SpawnPointsRed { get => spawnPointsRed;}
    int indexPlayer;
    string pTeam;

    Dictionary<string, Dictionary<int, GameObject>> spawnKeyPoints;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        instance = this;
        indexPlayer = (int)PhotonNetwork.LocalPlayer.CustomProperties["indexPlayer"];
        pTeam = PhotonNetwork.LocalPlayer.GetPhotonTeam().Name;
        InstantiatingPlayersCharacter();
    }

   
        private void InstantiatingPlayersCharacter()
    {
        int indexPlayer = (int)PhotonNetwork.LocalPlayer.CustomProperties["indexPlayer"];
        string pTeam = PhotonNetwork.LocalPlayer.GetPhotonTeam().Name;
        if (pTeam == "Blue")
        {
            PV.RPC("RPCInstantiateCharacter", PhotonNetwork.LocalPlayer, spawnPointsBlue[indexPlayer].transform.position, spawnPointsBlue[indexPlayer].transform.rotation);
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), 
            //SpawnPointsBlue[0].transform.position, SpawnPointsBlue[0].transform.rotation, 0);
        }
        if (pTeam == "Red")
        {
            PV.RPC("RPCInstantiateCharacter", PhotonNetwork.LocalPlayer, spawnPointsRed[indexPlayer].transform.position, spawnPointsRed[indexPlayer].transform.rotation);
        }

    }

    [PunRPC]
    void RPCInstantiateCharacter(Vector3 spawnPos, Quaternion spawnRot)
    {
        //myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), spawnPos, spawnRot, 0);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), spawnPos, spawnRot);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", PhotonNetwork.LocalPlayer.TagObject.ToString()), spawnPos, spawnRot);
        if (PhotonNetwork.LocalPlayer.TagObject.ToString() == "PlayerAvatar") id = 0;
        if (PhotonNetwork.LocalPlayer.TagObject.ToString() == "provisory") id = 1;
    }

    public Vector3 LocalPlayerSpawnPoint => (pTeam == "Blue") ? spawnPointsBlue[indexPlayer].transform.position : spawnPointsRed[indexPlayer].transform.position;



}
