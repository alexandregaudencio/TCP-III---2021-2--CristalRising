using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;
using System.Linq;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    public GameObject myAvatar;
    void Start()
    {
        
        PV = GetComponent<PhotonView>();
        for (int i = 0; i < PhotonNetwork.PlayerList.Length; i++)
        {
           if (PV.IsMine)
            {
                PV.RPC("RPCStartGame", PhotonNetwork.PlayerList[i], GameSetUp.GS.spawnPoints[i].position, GameSetUp.GS.spawnPoints[i].rotation);
            }
        }
       
    }

    [PunRPC]
    void RPCStartGame(Vector3 spawnPos, Quaternion spawnRot)
    {
        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar"), spawnPos, spawnRot,0);
    }
}
