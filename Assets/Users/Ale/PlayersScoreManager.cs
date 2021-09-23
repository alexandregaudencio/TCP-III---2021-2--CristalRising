using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayersScoreManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_Text playerNickText;
    [SerializeField] private TMP_Text killPointsText;
    [SerializeField] private TMP_Text deathPointsText;



    private ExitGames.Client.Photon.Hashtable HashProps = new ExitGames.Client.Photon.Hashtable();


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

        UpdateScoreUI();

        if (PhotonNetwork.LocalPlayer == targetPlayer)
        {
            
            //atualiza a propriedade os pontos de morte do player
            if (changedProps.ContainsKey("HP") && (int)targetPlayer.CustomProperties["HP"] <= 0 && !(bool)targetPlayer.CustomProperties["isDead"])
            {

                //HashProps["isDead"] = true;
                HashProps["deathCount"] = (int)targetPlayer.CustomProperties["deathCount"] + 1;
                PhotonNetwork.LocalPlayer.SetCustomProperties(HashProps);
            }

        }

    }

    private void UpdateScoreUI()
    {
        playerNickText.text = "";
        killPointsText.text = "";
        deathPointsText.text = "";

        foreach (Player player in PhotonNetwork.PlayerList)
        {
            playerNickText.text += player.NickName+"\n";
            killPointsText.text += (int)player.CustomProperties["killCount"] + "\n";
            deathPointsText.text += (int)player.CustomProperties["deathCount"]+ "\n";

        }

    }

    private void Start()
    {
        UpdateScoreUI();
    }
}
