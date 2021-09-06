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

    private Color GetTeamColor( byte teamCode)
    {
        return (teamCode == 1) ? RoomConfigs.instance.blueTeamColor : RoomConfigs.instance.redTeamColor;
    }

    Player[] playersTeamBlue;
    Player[] playersTeamRed;

    public float GetHPpercent(int hashHP, int hashMaxHP)
    {
        int hp = hashHP;
        int maxHP = hashMaxHP;
        return hp / maxHP;
    }

    void Start()
    {

        //TODO: RPC para poder ajustar em todas as máquinas.


        if (PV.IsMine)
        {
            localPlayer = Player;
            teamCode = localPlayer.GetPhotonTeam().Code;

            nicknameText.text = PV.Controller.NickName;
            HPOnTopImgfill.color = GetTeamColor(PV.Controller.GetPhotonTeam().Code);
            HPOnTopImgfill.fillAmount = GetHPpercent((int)localPlayer.CustomProperties["HP"], (int)localPlayer.CustomProperties["maxHP"]);
        }

    }


    //TODO: Enviar para todos a atualização da barra de vida???
    //[PunRPC]
    //private void RPCUpdateHPontopFill(Image imageHPfill)
    //{

    //}








    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == localPlayer && changedProps == localPlayer.CustomProperties["HP"])
        {
            HPOnTopImgfill.fillAmount = GetHPpercent((int)localPlayer.CustomProperties["HP"], (int)localPlayer.CustomProperties["maxHP"]);
            Debug.Log("O HP foi mudado.");
        }
    }







}
