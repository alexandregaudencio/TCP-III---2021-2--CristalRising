using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using System.Collections;

public class RoomManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TeamManager teamManager;
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Update()
    {

        //PARA TESTES
        if (Input.GetKeyDown(KeyCode.G))
        {
            if(PhotonNetwork.IsMasterClient) PhotonNetwork.LoadLevel(RoomConfigs.CharSelecSceneIndex);
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
    }


    public override void OnJoinedRoom()
    {
        teamManager.TeamDefinition(PhotonNetwork.LocalPlayer);

    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if ( PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
            {
                PhotonNetwork.CurrentRoom.IsOpen = false;
                PhotonNetwork.CurrentRoom.IsVisible = false;
                StartCoroutine(transitionToCharactSelectScene());
            }
        }
    }


    IEnumerator transitionToCharactSelectScene()
    {
        yield return new WaitForSeconds(1);
        PhotonNetwork.LoadLevel(RoomConfigs.CharSelecSceneIndex);

    }

}
