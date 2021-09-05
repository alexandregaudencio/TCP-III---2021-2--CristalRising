using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHUDLocalPlayerProps : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text HPText;
    [SerializeField] private Image HPImage;
    [SerializeField] private Image characterIcon;
    [SerializeField] private Image characterBorderIcon;
    [SerializeField] private TMP_Text charactername;
    [SerializeField] private TMP_Text characterAmmo;

    Player LocalPlayer;
    byte myTeam;

    // Start is called before the first frame update
    void Start()
    {
        LocalPlayer = Player;
        myTeam = LocalPlayer.GetPhotonTeam().Code;

        SetupLocalPlayerProps();
    }

    private Player Player
    {
        get
        {
            return PhotonNetwork.LocalPlayer;
        }
    }

    private Color TeamColor
    {
        get { return (myTeam == 1) ? RoomConfigs.instance.blueTeamColor : RoomConfigs.instance.redTeamColor; }
    }


    private float HPpercent
    {
        get
        {
            int hp = (int)LocalPlayer.CustomProperties["HP"];
            int maxHP = (int)LocalPlayer.CustomProperties["maxHP"];
            return hp / maxHP;
        }
    }

    private int HPValue
    {
        get
        {
            return  (int)LocalPlayer.CustomProperties["HP"];

        }
    }

    private string HPString
    {
        get
        {
            return HPValue.ToString() + " / " + LocalPlayer.CustomProperties["maxHP"].ToString();
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(LocalPlayer == targetPlayer && changedProps == targetPlayer.CustomProperties["HP"])
        {
            HPImage.fillAmount = HPpercent;
            HPText.text = HPString;
        }
    }

    private void SetupLocalPlayerProps()
    {
        int characterIndex = (int)LocalPlayer.CustomProperties["characterIndex"];
        //characterIcon.sprite = RoomConfigs.instance.charactersOrdered[characterIndex].characterIcon;
        characterBorderIcon.color = TeamColor;
        HPImage.fillAmount = HPpercent;
        HPText.text = HPString;
        charactername.text = RoomConfigs.instance.charactersOrdered[characterIndex].characterName;
        charactername.color = TeamColor;

    }


}
