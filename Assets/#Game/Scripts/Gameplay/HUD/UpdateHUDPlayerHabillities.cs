using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHUDPlayerHabillities : MonoBehaviourPunCallbacks
{
    //[SerializeField] private int indexHability;
    private byte team;
    private Image habilityIcon;

    private TMP_Text timerHability;

    [SerializeField] int habilityIndex;

    private void Start()
    {
        team = PhotonNetwork.LocalPlayer.GetPhotonTeam().Code;
        timerHability = GetComponentInChildren<TMP_Text>();
        habilityIcon = GetComponentInChildren<Image>();
        
        
        habilityIcon.color = SetHabilityColor;
        timerHability.text = "";

        SetCharacterHabilityIcon();
    }

    private Color SetHabilityColor
    {
        get {return (team == 1) ? RoomConfigs.instance.blueTeamColor : RoomConfigs.instance.redTeamColor;}
    }

    private void SetCharacterHabilityIcon()
    {
        int characterIndex = (int)PhotonNetwork.LocalPlayer.CustomProperties["characterIndex"];
        
        habilityIcon.sprite = RoomConfigs.instance.charactersOrdered[characterIndex].ordenedHabillityIcon[habilityIndex];
    }

    //public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    //{
    //    base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
    //}


}
