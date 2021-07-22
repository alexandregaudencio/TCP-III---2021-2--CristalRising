using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject findMatchBtn;
    [SerializeField] GameObject searchingPanel;
    [SerializeField] TMP_Text statusPlayerCountText;

  
    private void Start()
    {
        searchingPanel.SetActive(false);
        findMatchBtn.SetActive(false);
        //connect to the photon server
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("We are Connected to Photon! on " + PhotonNetwork.CloudRegion + " server");
        PhotonNetwork.AutomaticallySyncScene = true;
        findMatchBtn.SetActive(true);
    }
    public void FindMatch()
    {
        searchingPanel.SetActive(true);
        findMatchBtn.SetActive(false);

        //try to join a ore existing room - if it falls, create one
        PhotonNetwork.JoinRandomRoom();
        Debug.Log("Searching for a game");
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Could not Find Room - creating a room");
        MakeRoom();
    }
    void MakeRoom()
    {
        int randomRoomName = Random.Range(0, 5000);
        RoomOptions roomOptions =
            new RoomOptions()
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = 2
            };
        PhotonNetwork.CreateRoom("RoomName_" + randomRoomName, roomOptions);
        Debug.Log("Room create, Waiting For Another Player");

    }
    /*
     // count players(vou mexer nisso pra frente)
      public override void OnCreatedRoom()
    {
        StatusPlayerCount(PhotonNetwork.CurrentRoom.PlayerCount + "/2 ");
    }*/
    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
       // StatusPlayerCount(PhotonNetwork.CurrentRoom.PlayerCount + "/2 ");

        if (PhotonNetwork.CurrentRoom.PlayerCount == 2 && PhotonNetwork.IsMasterClient)
        {
             //Start game
            PhotonNetwork.LoadLevel(1);
        }
    }
    public void StopSearch()
    {
        searchingPanel.SetActive(false);
        findMatchBtn.SetActive(true);
        PhotonNetwork.LeaveRoom();
        Debug.Log("Stopped, Back to Menu");
        
    }
    private void StatusPlayerCount(string msg)
    {
        statusPlayerCountText.text = msg;
    }
}
