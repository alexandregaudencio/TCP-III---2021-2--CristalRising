using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using Photon.Pun.UtilityScripts;


public class LobbyController: MonoBehaviourPunCallbacks
{
    //public byte PlayersConectados;
    public static LobbyController instance;
    public GameObject LobbyCanvas;
    public GameObject StartButton;
    public GameObject RoomCanvas;
    //public Text NumeroJogadores;
    //public Text NomeSala;
    //public bool connected;
    //public GameObject PreRoomCanvas;
    //public byte NextPlayerTeam;
    //public PhotonView PV;
    //public byte RedTeam;
    //public byte BlueTeam;

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
            //DontDestroyOnLoad(gameObject);
        }
        //CanvasLoading.SetActive(true);
        //PhotonNetwork.AutomaticallySyncScene = true;
    }
    //private void Start()
    //{
    //    PhotonNetwork.ConnectUsingSettings();

       
    //}
    //private void Update()
    //{
    //    ////this.NumeroJogadores.text = PlayersConectados.ToString();
    //    //if(PhotonNetwork.InRoom)
    //    //PlayersConectados = PhotonNetwork.CurrentRoom.PlayerCount;
    //    //if (PhotonNetwork.IsConnected == true)
    //    //    this.connected = true;

    //}


    #region Photon Room Controller 


    //public override void OnConnectedToMaster()
    //{
    //    CanvasLoading.SetActive(false);
    //}
    //public void CreateRoom(string roomname)
    //{
    //    PhotonNetwork.CreateRoom(roomname);
    //    this.LobbyCanvas.SetActive(false);
    //    this.RoomCanvas.SetActive(true);
    //    Debug.Log("Estamos agora na sala, Jogadores Conectados: " + PlayersConectados);
    //}


    //public void JoinRoom()
    //{
    //    if (PhotonNetwork.IsConnected == true)
    //    {
    //        PhotonNetwork.JoinRandomRoom();
            
    //    }
    //    else
    //    {
    //        PhotonNetwork.ConnectUsingSettings();

    //    }
    //}


    public override void OnJoinedRoom()
    {
        if (this.LobbyCanvas.activeInHierarchy)
        {
            this.LobbyCanvas.SetActive(false);
            this.RoomCanvas.SetActive(true);
        }
        //if(PhotonNetwork.IsMasterClient)
        //{
        //    //ChoosingRedTeam();
        //    //startGameButton.SetActive(true);
        //}
        //else
        //{
        //    //PlayerAssister.T2.MyTeam = this.NextPlayerTeam;
        //}


        //if (PlayersConectados != RoomConfigs.maxRoomPlayers)
        //{
        //    Debug.Log("Esperando Players");
        //}
      
        //else
        //{


        //    Debug.Log("Pronto Pra Iniciar");

        //}

         Player[] players = PhotonNetwork.PlayerList;
         for (int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count(); i++)
         {
             Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }
    //public override void OnDisconnected(DisconnectCause cause)
    //{
    //    ChangeScene("Menu");
    //    PlayersConectados--;
    //}
    public void QuitRoom()
    {
        PhotonNetwork.LeaveRoom();
       // this.RoomCanvas.SetActive(false);
       // this.LobbyCanvas.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Um player desconectou");
        this.RoomCanvas.SetActive(false);
        this.LobbyCanvas.SetActive(true);
    }

   //private void ChangeScene(string sceneName)
   // {
   //    PhotonNetwork.LoadLevel(sceneName);
   // }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    #endregion

    #region Some other Methods
    //public void salvanomesala(string nomePlaceholder)
    //{
    //    NomeSala.text = nomePlaceholder;

    //}

    public void botaocriarsala()
    {
        this.LobbyCanvas.SetActive(false);
        //this.PreRoomCanvas.SetActive(true);
    }
    public void voltarButton()
    {
        this.LobbyCanvas.SetActive(true);
        //this.PreRoomCanvas.SetActive(false);
    }


    //public void ChoosingBlueTeam()
    //{
    //    Debug.Log("<color=blue>Escolhi time Azul</color>");
    //    NextPlayerTeam = 2;
    //    PlayerAssister.T2.CallGetTeam();
    //    this.BlueTeam++;
    //}

    //public void ChoosingRedTeam()
    //{
    //    Debug.Log("<color=red>Escolhi time Vermelho</color>");
    //    NextPlayerTeam = 1;
    //    PlayerAssister.T2.CallGetTeam();
    //    this.RedTeam++;
    //}

    //public void TrocarCenaJogo()
    //{
    //    SceneManager.LoadScene("Oficial_CharacterSelection");
    //}








    #endregion


}





