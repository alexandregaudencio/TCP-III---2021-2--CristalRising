using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class SceneController : MonoBehaviourPunCallbacks
{
    public TMP_Text timeToDisplay;

    private bool startingGame = false;
    private PhotonView PV;
    private TimerCountdown waintingRoomTimer;
    private void Start()
    {
        PV = GetComponent<PhotonView>();
        waintingRoomTimer = GetComponent<TimerCountdown>();
        waintingRoomTimer.CurrentTime = RoomConfigs.characterSelectionMaxTime;
        
    }

    // Update is called once per frame
    private void Update()
    {
        UIUpdate();

        if (waintingRoomTimer.IsCountdownOver())
        {
            if (startingGame) return;
            StartGame();
        }
    }

    private void UIUpdate()
    {
        string tempTimer = string.Format("{0:00}", waintingRoomTimer.CurrentTime);
        timeToDisplay.text = tempTimer;
    }

    public void StartGame()
    {
        startingGame = true;
        //aqui
        if (!PhotonNetwork.IsMasterClient) return;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        PhotonNetwork.LoadLevel(RoomConfigs.gameplaySceneIndex);
        
    }


    //não sei se ta certo fazer isso aqui em ONPLAYERENTEREDROOM
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("SendTimer", RpcTarget.Others, waintingRoomTimer.CurrentTime);
        }
        base.OnPlayerEnteredRoom(newPlayer);
    }

    [PunRPC]
    private void SendTimer(float timeIn)
    {
        waintingRoomTimer.CurrentTime = timeIn;
    }
    //public void DelayCancel()
    //{
    //    PhotonNetwork.LeaveRoom();
    //    SceneManager.LoadScene(RoomConfigs.menuSceneIndex);
    //}
}
