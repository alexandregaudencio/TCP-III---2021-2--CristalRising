using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateTeamHealthbar : MonoBehaviourPunCallbacks
{
    [SerializeField] int indexPlayer;
    [SerializeField] byte team;
    
    
    Player[] teamMembers;

    Image healthbarImg;
    Player currentPlayer;

    void Start()
    {
        healthbarImg = GetComponent<Image>();
        currentPlayer = getPlayer();
        ResetTeamProps(this.currentPlayer);

        //if(currentPlayer == null)
        //{
        //    currentPlayer = getPlayer();
        //}
    }

    private Player getPlayer()
    {
        PhotonTeamsManager.Instance.TryGetTeamMembers(team, out teamMembers);
            return teamMembers[indexPlayer];
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

        if(targetPlayer == currentPlayer && changedProps == targetPlayer.CustomProperties["HP"])
        {
            float hp = (float)targetPlayer.CustomProperties["HP"];
            float maxHP = (float)targetPlayer.CustomProperties["maxHP"];
            healthbarImg.fillAmount = hp /maxHP;
        }
    }


    void ResetTeamProps(Player currentPlayer)
    {
      if(currentPlayer != null && currentPlayer.CustomProperties.ContainsKey("HP"))
        {
            float hp = (float)currentPlayer.CustomProperties["HP"];
            float maxHP = (float)currentPlayer.CustomProperties["maxHP"];
            healthbarImg.fillAmount = hp / maxHP;
        }
    }


    //private void OnGUI()
    //{
    //    if (currentPlayer.CustomProperties.ContainsKey("HP"))
    //    {
    //        ////PROVISÓRIO
    //        GUILayout.BeginHorizontal();
    //        GUILayout.Label("HP" + indexPlayer + ": " + (int)currentPlayer.CustomProperties["HP"]);
    //        //GUILayout.Label("maxHP" + indexPlayer + ": " + (int)currentPlayer.CustomProperties["maxHP"]);
    //        //GUILayout.Label("maxAmmo" + indexPlayer + ": " + (int)currentPlayer.CustomProperties["maxAmmo"]);

    //        GUILayout.EndHorizontal();
    //    }
    //}








}
