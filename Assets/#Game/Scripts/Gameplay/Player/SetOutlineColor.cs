using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

public class SetOutlineColor : MonoBehaviour
{
    byte teamCode;
    Outline outline;
    private Color TeamColor => (teamCode == 1) ? RoomConfigs.instance.blueTeamColor : RoomConfigs.instance.redTeamColor;
    PhotonView PV;

    Player[] playersTeamBlue;
    Player[] playersTeamRed;


    void Start()
    {
        outline = GetComponent<Outline>();
        //PV = GetComponent<PhotonView>();
        teamCode = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code;


        PhotonTeamsManager.Instance.TryGetTeamMembers("Blue", out playersTeamBlue);
        PhotonTeamsManager.Instance.TryGetTeamMembers("Red", out playersTeamRed);

        foreach (Player p in playersTeamBlue)
            if (GetComponent<PhotonView>().Controller.Equals(p))
            {
                GetComponent<Outline>().OutlineColor = RoomConfigs.instance.blueTeamColor;
                //teamIdentify.GetComponent<Renderer>().material.SetColor(RoomConfigs.instance.blueTeamColor);
                gameObject.layer = LayerMask.NameToLayer("Team1");
            }
        foreach (Player p in playersTeamRed)
            if (GetComponent<PhotonView>().Controller.Equals(p))
            {
                GetComponent<Outline>().OutlineColor = RoomConfigs.instance.redTeamColor;
                //teamIdentify.GetComponent<Renderer>().material.SetColor(RoomConfigs.instance.redTeamColor);
                gameObject.layer = LayerMask.NameToLayer("Team2");
            }
    }



}
