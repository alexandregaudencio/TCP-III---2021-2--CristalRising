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
    public static SetUpGameplay instance;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    private int indexPlayerList;

    void Start()
    {
        instance = this;
        InstantiatingPlayersCharacter();
        //SetIndexPlayer();
    }

    //private void SetIndexPlayer()
    //{
    //    byte team = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code;
    //    Player[] playerTeamList;
    //    PhotonTeamsManager.Instance.TryGetTeamMembers(team, out playerTeamList);
    //    foreach (Player player in playerTeamList)
    //    {
    //        if (player == PhotonNetwork.LocalPlayer)
    //        {
    //            Debug.Log("Parou aaqui? " + indexPlayerList);
    //            return;
    //        }
    //        indexPlayerList++;
    //        Debug.Log(indexPlayerList);
    //    }
    //}
    private void InstantiatingPlayersCharacter()
    {
        int indexPlayer = (int)PhotonNetwork.LocalPlayer.CustomProperties["indexPlayer"];
        string pTeam = PhotonNetwork.LocalPlayer.GetPhotonTeam().Name;
        if (pTeam == "Blue")
        {
            PV.RPC("RPCInstantiateCharacter", PhotonNetwork.LocalPlayer, SpawnPointsBlue[indexPlayer].transform.position, SpawnPointsBlue[indexPlayer].transform.rotation);
            //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), 
            //SpawnPointsBlue[0].transform.position, SpawnPointsBlue[0].transform.rotation, 0);
        }
        if (pTeam == "Red")
        {
            PV.RPC("RPCInstantiateCharacter", PhotonNetwork.LocalPlayer, SpawnPointsRed[indexPlayer].transform.position, SpawnPointsRed[indexPlayer].transform.rotation);
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
