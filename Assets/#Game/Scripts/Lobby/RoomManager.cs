using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

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
            if(PhotonNetwork.IsMasterClient) DefineTeamAndGo();
        }

        //if( PhotonNetwork.CurrentRoom.PlayerCount == PhotonNetwork.CurrentRoom.MaxPlayers)
        //{
        //    DefineTeamAndGo();
        //}
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
        PhotonNetwork.LoadLevel(RoomConfigs.CharSelecSceneIndex);
    }

    public override void OnJoinedRoom()
    {

        teamManager.TeamDefinition(PhotonNetwork.LocalPlayer);
        base.OnJoinedRoom();
    }


}
