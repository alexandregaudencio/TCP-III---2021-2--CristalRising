using Photon.Pun;
using UnityEngine;
using Photon.Realtime;

public class RoomManager : MonoBehaviourPunCallbacks
{
    //[SerializeField] private int maxPlayersRoom;

    //private DefineTeam defineTeam;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        //defineTeam = GetComponent<DefineTeam>();
    }

    private void Update()
    {
        //PARA TESTES
        if (Input.GetKeyDown(KeyCode.G))
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
       GetComponent<DefineTeam>().TeamDefinition();
        PhotonNetwork.LoadLevel(RoomConfigs.CharSelecSceneIndex);
    }

    public override void OnJoinedRoom()
    {
        //if(PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == RoomConfigs.maxRoomPlayer)
        ////if(PhotonNetwork.IsMasterClient &&        if (PhotonNetwork.CurrentRoom.PlayerCount == RoomConfigs.maxRoomPlayers)
        ////    {
        ////        DefineTeamAndGo();
        ////    }
        //if(PhotonNetwork.CurrentRoom.PlayerCount = )
        base.OnJoinedRoom();
    }



}
