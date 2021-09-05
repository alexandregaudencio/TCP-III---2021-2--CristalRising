using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun;

public class UpdateHUDPlayersProps : MonoBehaviourPunCallbacks
{
    /*[SerializeField] */
    private TMP_Text textTimerRespawn;


    Image playerCharacterImg;
    UpdateTeamHealthbar scriptTeamHealtbar;

    private void Awake()
    {

        playerCharacterImg = GetComponent<Image>();
        textTimerRespawn = GetComponentInChildren<TMP_Text>();

    }

    private void Start()
    {
        scriptTeamHealtbar = GetComponentInChildren<UpdateTeamHealthbar>();
        textTimerRespawn.text = "";
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == scriptTeamHealtbar.CurrentPlayer && changedProps == targetPlayer.CustomProperties["isDead"])
        {
            CheckPlayersTimeProps((bool)targetPlayer.CustomProperties["isDead"], (string)targetPlayer.CustomProperties["timerRespawn"]);
            CheckPlayersCharacterIcon((bool)targetPlayer.CustomProperties["isDead"]);
        }
    }

    private void CheckPlayersCharacterIcon(bool isDead)
    {
        playerCharacterImg.color = (isDead) ? new Color(100, 100, 100, 200) : new Color(255, 255, 255, 200);

    }


    private void CheckPlayersTimeProps(bool isDead, string timerToText)
    {
      textTimerRespawn.text =  (isDead) ? timerToText :  "";
    }


}
