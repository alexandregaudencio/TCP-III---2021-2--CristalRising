
using Photon.Pun.UtilityScripts;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TeamManager : PhotonTeamsManager
{
    


    public void TeamDefinition(Player newPlayer)
    {
        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if(playerCount%2 == 1)
        {
            newPlayer.JoinTeam("Blue");
        } else
        {
            newPlayer.JoinTeam("Red");
        }

        DontDestroyOnLoad(gameObject);
        //for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        //{
        //    if(i%2 == 0) PhotonNetwork.PlayerList[i].JoinTeam("Blue");
        //    else         PhotonNetwork.PlayerList[i].JoinTeam("Blue");
        //}

    }

    void OnGUI()
    {
        Player[] playersTeamBlue;
        Player[] playersTeamRed;

        Instance.TryGetTeamMembers(1, out playersTeamBlue);
        Instance.TryGetTeamMembers(2, out playersTeamRed);

        GUILayout.BeginVertical();
        GUILayout.Label("Team Blue");
        foreach (Player p in playersTeamBlue)
        {
            GUILayout.Button(p.NickName/*+" "+p.GetPhotonTeam()*/);
        }
        GUILayout.Label("Team Red");
        foreach (Player p in playersTeamRed)
        {
            GUILayout.Button(p.NickName/*+" "+p.GetPhotonTeam()*/);
        }

        GUILayout.EndVertical();

    }




}
