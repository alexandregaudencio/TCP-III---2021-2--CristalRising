using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Pun;
using Photon.Realtime;

public class RealtimeCharacterSelect : MonoBehaviourPunCallbacks
{
    PhotonTeamsManager ptm;

    void OnGUI()
    {
        Player[] playersTeamBlue;
        Player[] playersTeamRed;

        ptm.TryGetTeamMembers(1, out playersTeamBlue);
        ptm.TryGetTeamMembers(2, out playersTeamRed);

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

