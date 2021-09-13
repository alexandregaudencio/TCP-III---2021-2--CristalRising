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

    private ExitGames.Client.Photon.Hashtable HashProps = new ExitGames.Client.Photon.Hashtable();

   
    private void Awake()
    {

        playerCharacterImg = GetComponent<Image>();
        textTimerRespawn = GetComponentInChildren<TMP_Text>();

    }

    private void Start()
    {
            //TODO: ajustar para acessar o scriptTeamH em tempo realtime
            //Debug.Log(scriptTeamHealtbar.instance.CurrentPlayer);
            //DisablePlayerProp();
       

            scriptTeamHealtbar = GetComponentInChildren<UpdateTeamHealthbar>();
        textTimerRespawn.text = "";
    }

    void DisablePlayerProp()
    {
        playerCharacterImg.color = RoomConfigs.instance.noneColor;
        //scriptTeamHealtbar.HealthbarImg.fillAmount = 0f;
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if (targetPlayer == scriptTeamHealtbar.CurrentPlayer)
        {

            if(changedProps.ContainsKey("isDead"))
            {
                SwitchPlayersAlive((bool)scriptTeamHealtbar.CurrentPlayer.CustomProperties["isDead"]);

            }
            if (changedProps.ContainsKey("timerRespawn"))
            {
                UpdatePlayersTimeRespawn();
            }

        }
    }


    private void SwitchPlayersAlive(bool isDead)
    {
        textTimerRespawn.gameObject.SetActive(isDead);
        //playerCharacterImg.color = (isDead) ? new Color(100, 100, 100, 200) : new Color(255, 255, 255, 200);

    }


    private void UpdatePlayersTimeRespawn()
    {
        textTimerRespawn.text = scriptTeamHealtbar.CurrentPlayer.CustomProperties["timerRespawn"].ToString();
    }


}
