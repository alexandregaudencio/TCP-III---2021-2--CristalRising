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

    Player localPlayer;
    byte myTeam;

    // Start is called before the first frame update
    void Start()
    {
        localPlayer = Player;
        myTeam = localPlayer.GetPhotonTeam().Code;

        SetupLocalPlayerProps();
    }

    private Player Player => PhotonNetwork.LocalPlayer;

    private Color TeamColor
    {
        get { return (myTeam == 1) ? RoomConfigs.instance.blueTeamColor : RoomConfigs.instance.redTeamColor; }
    }


    public float HPpercent
    {
        get
        {
            int hp = (int)localPlayer.CustomProperties["HP"];
            int maxHP = (int)localPlayer.CustomProperties["maxHP"];
            //float result = (float)hp / maxHP;
            return (float)hp / maxHP;
        }
    }

    private int HPValue
    {
        get
        {
           return Mathf.Clamp((int)localPlayer.CustomProperties["HP"], 0, (int)localPlayer.CustomProperties["maxHP"]);

        }
    }

    private string HPString
    {
        get
        {
            return HPValue.ToString() + " / " + localPlayer.CustomProperties["maxHP"].ToString();
        }
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(localPlayer == targetPlayer /*&&*/ /*changedProps.Keys.Equals("HP")*/)
        {
            HPImage.fillAmount = HPpercent;
            HPText.text = HPString;
        }
    }

    private void SetupLocalPlayerProps()
    {
        int characterIndex = (int)localPlayer.CustomProperties["characterIndex"];
        //characterIcon.sprite = RoomConfigs.instance.charactersOrdered[characterIndex].characterIcon;
        characterBorderIcon.color = TeamColor;
        HPImage.fillAmount = HPpercent;
        HPText.text = HPString;
        charactername.text = RoomConfigs.instance.charactersOrdered[characterIndex].characterName;
        charactername.color = TeamColor;

    }


}
