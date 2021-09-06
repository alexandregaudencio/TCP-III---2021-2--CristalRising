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
    private Player currentPlayer;
    public Player CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }

    private Player Player
    {
        get
        {
            PhotonTeamsManager.Instance.TryGetTeamMembers(team, out teamMembers);
            return indexPlayer < teamMembers.Length ? teamMembers[indexPlayer] : null;
        }
    }


    void Start()
    {
        healthbarImg = GetComponent<Image>();
        CurrentPlayer = Player;
        ResetTeamProps(this.CurrentPlayer);


    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

        if(targetPlayer == CurrentPlayer && changedProps == targetPlayer.CustomProperties["HP"])
        {
            healthbarImg.fillAmount = HealthbarAmmount;
        }
    }


    void ResetTeamProps(Player currentPlayer)
    {
      if(currentPlayer != null && currentPlayer.CustomProperties.ContainsKey("HP"))
        {
            healthbarImg.fillAmount = HealthbarAmmount;
        }
    }


    private float HealthbarAmmount
    {
        get
        {
            int hp = (int)CurrentPlayer.CustomProperties["HP"];
            int maxHP = (int)CurrentPlayer.CustomProperties["maxHP"];
            return (float)hp / maxHP;
        }
    }
}
