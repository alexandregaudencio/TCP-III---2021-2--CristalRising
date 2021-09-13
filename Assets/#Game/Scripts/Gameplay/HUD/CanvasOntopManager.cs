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
    
    /*[SerializeField]*/ private PhotonView PV;
    [SerializeField] private Image HPOnTopImgfill;
    [SerializeField] private TMP_Text nicknameText;
    private Player player;
    //Player Player;

    private Color GetTeamColor( byte teamCode)
    {
        return (teamCode == 1) ? RoomConfigs.instance.blueTeamColor : RoomConfigs.instance.redTeamColor;
    }

    //Player[] playersTeamBlue;
    //Player[] playersTeamRed;

    public float HPpercent
    {
        get
        {
            int hp = (int)player.CustomProperties["HP"];
            int maxHP = (int)player.CustomProperties["maxHP"];
            //float result = (float)hp / maxHP;
            return (float)hp / maxHP;
        }
    }

    void Start()
    {
        player  = PhotonNetwork.LocalPlayer;
        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            teamCode = player.GetPhotonTeam().Code;
            PV.RPC("RpcSetupCanvasOntop", RpcTarget.All, player.NickName, teamCode);
            PV.RPC("RPCUpdateHPontopFill", RpcTarget.All, HPpercent);
            //PV.RPC("RPCUpdateHPontopFill", RpcTarget.All, 0.8f , Player.NickName);
        }

    }

    //private void LerpFillAmmount(Image image)
    //{
    //    image.fillAmount = Mathf.Lerp(
    //        image.fillAmount,
    //        GetHPpercent((int)localPlayer.CustomProperties["HP"], (int)localPlayer.CustomProperties["maxHP"]),
    //        Time.fixedDeltaTime*4);
    //}



    //TODO: Enviar para todos a atualização da barra de vida???
    [PunRPC]
    private void RPCUpdateHPontopFill(float HPpercent)
    {
        HPOnTopImgfill.fillAmount = HPpercent;
    }
    [PunRPC]
    private void RpcSetupCanvasOntop(string nick, byte team)
    {
        nicknameText.text = nick;
        HPOnTopImgfill.color = GetTeamColor(team);


    }



    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == player && PV.Controller == targetPlayer)
        {
            PV.RPC("RPCUpdateHPontopFill", RpcTarget.All, HPpercent);
        }
    }







}
