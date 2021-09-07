
using Photon.Pun.UtilityScripts;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TeamManager : PhotonTeamsManager
{
    private ExitGames.Client.Photon.Hashtable hasIndexPlayer = new ExitGames.Client.Photon.Hashtable();
    Player[] playersTeam;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void TeamDefinition(Player newPlayer)
    {
        

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
        if(playerCount%2 == 1)
        {
            newPlayer.JoinTeam(2);
            TryGetTeamMembers(2, out playersTeam);
            SetIndexPlayerProp(newPlayer, playersTeam.Length);

        } else
        {
            newPlayer.JoinTeam(1);
            TryGetTeamMembers(1, out playersTeam);
            SetIndexPlayerProp(newPlayer, playersTeam.Length);


        }

        //for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        //{
        //    if(i%2 == 0) PhotonNetwork.PlayerList[i].JoinTeam("Blue");
        //    else         PhotonNetwork.PlayerList[i].JoinTeam("Blue");
        //}

    }

    //TODO: FAZER EM RELAÇÃO AO TIME    
    public void SetIndexPlayerProp(Player newPlayer, int index)
    {
        hasIndexPlayer["indexPlayer"] = index;
        newPlayer.SetCustomProperties(hasIndexPlayer);
    }

    //void OnGUI()
    //{

    //    Instance.TryGetTeamMembers(1, out playersTeam);
    //    Instance.TryGetTeamMembers(2, out playersTeamRed);


    //    ////PROVISÓRIO
    //    //GUILayout.BeginVertical();
    //    //    GUILayout.BeginHorizontal();
    //    //    GUILayout.Label("Team Blue");
    //    //    foreach (Player p in playersTeamBlue){GUILayout.Button(p.NickName);}
    //    //     GUILayout.EndHorizontal();

    //    //    GUILayout.BeginHorizontal();
    //    //    GUILayout.Label("Team Red");
    //    //    foreach (Player p in playersTeamRed) { GUILayout.Button(p.NickName); }
    //    //    GUILayout.EndHorizontal();
    //    //GUILayout.EndVertical();

        


    //}




}
