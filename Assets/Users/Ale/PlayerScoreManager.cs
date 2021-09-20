using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScoreManager : MonoBehaviourPunCallbacks
{
    private ExitGames.Client.Photon.Hashtable HashProps = new ExitGames.Client.Photon.Hashtable();

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (PhotonNetwork.LocalPlayer == targetPlayer)
        {
            if(changedProps.ContainsKey("HP") && (int)targetPlayer.CustomProperties["HP"] <= 0)
            {

                HashProps["isDead"] = true;
                HashProps["deathCount"] = (string)targetPlayer.CustomProperties["deathCount"] + 1;
                PhotonNetwork.LocalPlayer.SetCustomProperties(HashProps);
            }

        }

    }


}
