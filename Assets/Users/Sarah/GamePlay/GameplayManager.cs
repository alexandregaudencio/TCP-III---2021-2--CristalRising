using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    public TMP_Text timeToDisplay;

    private bool endingGame = false;
    //private PhotonView PV;
    private TimerCountdown gameplayRoomTimer;
    private void Start()
    {
        //PV = GetComponent<PhotonView>();
        gameplayRoomTimer = GetComponent<TimerCountdown>();
        gameplayRoomTimer.CurrentTime = RoomConfigs.gameplayMaxTime;

    }

    private void Update()
    {
        UIUpdate();

        if (gameplayRoomTimer.IsCountdownOver())
        {
            if (endingGame) return;
            EndGame();
        }
    }

    private void UIUpdate()
    {
        string tempTimer = string.Format("{0:00}", gameplayRoomTimer.CurrentTime);
        timeToDisplay.text = tempTimer;
    }

    public void EndGame()
    {
        endingGame = true;
        Debug.Log("Acabou o jogo");
        //aqui
        //if (!PhotonNetwork.IsMasterClient) return;
        ///PhotonNetwork.CurrentRoom.IsOpen = false;

    }

    /* public override void OnPlayerEnteredRoom(Player newPlayer)
     {

         if (PhotonNetwork.IsMasterClient)
         {
             PV.RPC("SendTimer", RpcTarget.Others, gameplayRoomTimer.CurrentTime);
         }
         base.OnPlayerEnteredRoom(newPlayer);
     }

     [PunRPC]
     private void SendTimer(float timeIn)
     {
         gameplayRoomTimer.CurrentTime = timeIn;
     }
     */
}
