using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;
using TMPro;

public class DefineMorte : MonoBehaviourPunCallbacks
{
    //public GameObject[] SpawnPointsTimeAzul;
    //public GameObject[] SpawnPointTimeVermelho;
    //PlayerProperty PP;
    //public float TemporizadorRespawn;
    //public bool SpawnCheck;
    //PhotonTeam TimeDesteJogador;
    public GameObject CanvasDeMorte;
    public Text TextoContador;
    private GameObject HUDCanvas;

    //[SerializeField] private float avada;
    //SetUpGameplay setUpGameplay;

    private ExitGames.Client.Photon.Hashtable HashDeadProps = new ExitGames.Client.Photon.Hashtable();
    private ExitGames.Client.Photon.Hashtable HashResetProps = new ExitGames.Client.Photon.Hashtable();


    private void Start()
    {
        //setUpGameplay = FindObjectOfType<SetUpGameplay>();
        CanvasDeMorte.gameObject.SetActive(false);
        //PP = GetComponent<PlayerProperty>();
        HUDCanvas = GameObject.Find("HUD_Canvas");
        GetComponent<Controle>().AmmoText = GameObject.Find("AmmoText");
        GetComponent<Controle>().UpdateAmmoText();
    }


    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(targetPlayer == PhotonNetwork.LocalPlayer)
        {
           if(changedProps.ContainsKey("HP") && (int)targetPlayer.CustomProperties["HP"] <= 0 && !(bool)targetPlayer.CustomProperties["isDead"]) {
                StartCoroutine(deathEvent());
           }

           if(changedProps.ContainsKey("isDead"))
            {
                SwitchObjectsOnDeath((bool)targetPlayer.CustomProperties["isDead"]);
            }
        }

    }

    IEnumerator deathEvent()
    {
        HashDeadProps["isDead"] = true;
        int countdown = RoomConfigs.instance.timeToRespawn;
        
        //contador
        while (countdown > 0)
        {
            HashDeadProps["timerRespawn"] = countdown;
            PhotonNetwork.LocalPlayer.SetCustomProperties(HashDeadProps);
            TextoContador.text = countdown.ToString();
            yield return new WaitForSeconds(1);
            countdown--;
        }

        HashDeadProps["isDead"] = false;
        PhotonNetwork.LocalPlayer.SetCustomProperties(HashDeadProps);
        ResetPlayerProps();
        ResetCharacterProps();

    }

    private void SwitchObjectsOnDeath(bool deadState)
    {
        CanvasDeMorte.SetActive(deadState);
        GetComponent<PlayerController>().enabled = !deadState;
        HUDCanvas.SetActive(!deadState);

    }


    private void ResetCharacterProps(/*bool boolean*/)
    {
        transform.position = SetUpGameplay.instance.LocalPlayerSpawnPoint;
        
    }

    private void ResetPlayerProps()
    {
        HashResetProps["HP"] = (int)PhotonNetwork.LocalPlayer.CustomProperties["maxHP"];
        PhotonNetwork.LocalPlayer.SetCustomProperties(HashResetProps);
    }




}
