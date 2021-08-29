
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;
using ExitGames.Client.Photon;

public class LobbyController: MonoBehaviourPunCallbacks
{
    public static LobbyController instance;
    public GameObject LobbyCanvas;
    public GameObject RoomCanvas;
    
    public Text playersCountText;

    //playerName
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] Transform playerListContent;

    void Awake()
    {
        
        if (instance != null && instance == this)
            gameObject.SetActive(false);
        else
        {
            instance = this;
        }
    }


    public override void OnJoinedRoom()
    {
        if (this.LobbyCanvas.activeInHierarchy)
        {
            this.LobbyCanvas.SetActive(false);
            this.RoomCanvas.SetActive(true);
        }

        //int currentRoom = PhotonNetwork.PlayerList.Length;
        //playersCountText.text = PhotonNetwork.PlayerList.Length.ToString();

        //ESSA LINHA AQUI NÃO TA LEGAL:
        playersCountText.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;

        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count(); i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        //ESSA LINHA AQUI NÃO TA LEGAL:
        playersCountText.text = PhotonNetwork.CurrentRoom.PlayerCount + " / " + PhotonNetwork.CurrentRoom.MaxPlayers;


        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }

    public void QuitRoom()
    {
        PhotonNetwork.LeaveRoom();
        // this.RoomCanvas.SetActive(false);
        // this.LobbyCanvas.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Desconectou.");
        this.RoomCanvas.SetActive(false);
        this.LobbyCanvas.SetActive(true);
    }


    //public void botaocriarsala()
    //{
    //    this.LobbyCanvas.SetActive(false);
    //    //this.PreRoomCanvas.SetActive(true);
    //}
    //public void voltarButton()
    //{
    //    this.LobbyCanvas.SetActive(true);
    //    //this.PreRoomCanvas.SetActive(false);
    //}



}





