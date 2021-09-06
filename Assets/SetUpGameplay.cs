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
    [SerializeField] private GameObject[] SpawnPointsBlue, SpawnPointsRed;
    [SerializeField] public GameObject[] Characters;

    public static SetUpGameplay instance;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    void Start()
    {
        instance = this;

        InstantiatingPlayersCharacter();
    }

    private void InstantiatingPlayersCharacter()
    {

            string pTeam = PhotonNetwork.LocalPlayer.GetPhotonTeam().Name;
            

            if (pTeam == "Blue")
            {
                PV.RPC("RPCInstantiateCharacter", PhotonNetwork.LocalPlayer, SpawnPointsBlue[0].transform.position, SpawnPointsBlue[0].transform.rotation);
                //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), 
                //SpawnPointsBlue[0].transform.position, SpawnPointsBlue[0].transform.rotation, 0);
            }
            if (pTeam == "Red")
            {
                PV.RPC("RPCInstantiateCharacter", PhotonNetwork.LocalPlayer, SpawnPointsRed[0].transform.position, SpawnPointsRed[0].transform.rotation);
            }

     
    }

    [PunRPC]
    void RPCInstantiateCharacter(Vector3 spawnPos, Quaternion spawnRot)
    {
        //myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), spawnPos, spawnRot, 0);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), spawnPos, spawnRot);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", PhotonNetwork.LocalPlayer.TagObject.ToString()), spawnPos, spawnRot);
    }
}
