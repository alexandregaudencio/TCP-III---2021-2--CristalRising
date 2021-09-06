using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

public class CanvasOntopManager : MonoBehaviourPunCallbacks
{
    byte teamCode;
    
    [SerializeField] private PhotonView PV;
    [SerializeField] private Image HPOnTopImgfill;
    [SerializeField] private TMP_Text nicknameText;
    private Player Player => PhotonNetwork.LocalPlayer;
    Player localPlayer;

    private Color TeamColor
    {
        get { return (teamCode == 1) ? RoomConfigs.instance.blueTeamColor : RoomConfigs.instance.redTeamColor; }
    }

    public float HPpercent
    {
        get
        {
            int hp = (int)localPlayer.CustomProperties["HP"];
            int maxHP = (int)localPlayer.CustomProperties["maxHP"];
            return hp / maxHP;
        }
    }

    void Start()
    {
        localPlayer = Player;
        teamCode = localPlayer.GetPhotonTeam().Code;
        
        //TODO: RPC para poder ajustar em todas as máquinas.
        nicknameText.text = localPlayer.NickName;
        HPOnTopImgfill.color = TeamColor;
        HPOnTopImgfill.fillAmount = HPpercent;

    }



    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == localPlayer && changedProps == localPlayer.CustomProperties["HP"])
        {
            HPOnTopImgfill.fillAmount = HPpercent;
            Debug.Log("O HP foi mudado.");
        }
    }





}
