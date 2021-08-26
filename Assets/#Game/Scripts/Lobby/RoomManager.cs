using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    //[SerializeField] private int maxPlayersRoom;

    private DefineTeam defineTeam;

    private void Awake()
    {
        defineTeam = GetComponent<DefineTeam>();
    }

    private void Update()
    {
        //PARA TESTES
        if (Input.GetKeyDown(KeyCode.Y))
        {
            DefineTeamAndGo();
        }

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == RoomConfigs.maxRoomPlayers)
        {
            DefineTeamAndGo();
        }
    }


    //AÇÃO BOTÃO START
    public void StartGame()
    {
        PhotonNetwork.JoinRandomRoom();
    }


    public override void OnJoinRandomFailed(short returnCode, string message)
    {

        string roomName = Random.Range(0, 2000).ToString();
        RoomOptions roomOptions = new RoomOptions()
        {
            MaxPlayers = RoomConfigs.maxRoomPlayers,
            IsOpen = true
        };
        PhotonNetwork.CreateRoom(roomName, roomOptions);

        base.OnJoinRandomFailed(returnCode, message);
    }

    public void DefineTeamAndGo()
    {
        defineTeam.TeamDefinition();
        PhotonNetwork.LoadLevel(RoomConfigs.CharSelecSceneIndex);
    }


}
