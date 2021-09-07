using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayersCharactersVisualizing : MonoBehaviourPunCallbacks
{

    [SerializeField] private Image[] PlayersCharacterIcon;
    [SerializeField] private TMP_Text[] PlayersNickText;

    //[SerializeField] private Sprite[] charactersIconToSet;

    private Player[] myTeamMembers;
    private PhotonTeam myTeam;

    void Start()
    {
        myTeam = PhotonTeamExtensions.GetPhotonTeam(PhotonNetwork.LocalPlayer);
        PhotonTeamsManager.Instance.TryGetTeamMembers(myTeam.Code, out myTeamMembers);


        for (int i = 0; i < myTeamMembers.Length; i++)
        {
            PlayersNickText[i].text =  myTeamMembers[i].NickName;
            PlayersCharacterIcon[i].sprite = RoomConfigs.instance.charactersOrdered[0].characterIcon;
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        for (int i = 0; i < myTeamMembers.Length; i++)
        {
            if(myTeamMembers[i] == targetPlayer)
            {
                int indexImg = (int)targetPlayer.CustomProperties["characterIndex"];
                PlayersCharacterIcon[i].sprite = RoomConfigs.instance.charactersOrdered[indexImg].characterIcon;
            }
        }        
       
    }

}

