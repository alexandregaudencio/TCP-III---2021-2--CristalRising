using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
//using TMPro;
using UnityEngine.UI;

public class ShowPlayerTeamMates : PhotonTeamsManager
{


    void OnGUI()
    {
        Player[] playersTeamBlue;
        Player[] playersTeamRed;

        TryGetTeamMembers(1, out playersTeamBlue);
        TryGetTeamMembers(2, out playersTeamRed);

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
