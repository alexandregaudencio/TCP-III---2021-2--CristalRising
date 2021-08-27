//using Photon.Pun.UtilityScripts;
//using Photon.Pun;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Photon.Pun.UtilityScripts;

//public class TeamManager : PhotonTeamsManager
//{
//    [SerializeField] private byte myTeam = 0;
//    //[SerializeField] private string teamNameA;
//    //[SerializeField] private string teamNameB;


//    void Start()
//    {
//        PhotonTeam team = new PhotonTeam();
//        myTeam = team.Code;
//        PhotonTeam[] x = GetAvailableTeams();
//        for(int i = 0; i < x.Length; i++)
//        {
//            Debug.Log(x[i]);
//        }

//        if(LobbyController.instance.photonView.IsMine)
//        {
            
//        }
        
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
