using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class LobbyController: MonoBehaviourPunCallbacks
{
    public byte PlayersConectados, MaxPlayers;
    public static LobbyController instance;
    public GameObject LobbyCanvas;
    public GameObject StartButton;
    public GameObject RoomCanvas;
    public Text NumeroJogadores;
    public Text NomeSala;
    public bool connected;
    public GameObject PreRoomCanvas;

    //playerName
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] Transform playerListContent;

    void Awake()
    {
        


        MaxPlayers = 2;
        if (instance != null && instance == this)
            gameObject.SetActive(false);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
       
    }
    private void Update()
    {
        this.NumeroJogadores.text = PlayersConectados.ToString();
        if(PhotonNetwork.InRoom)
        PlayersConectados = PhotonNetwork.CurrentRoom.PlayerCount;
        if (PhotonNetwork.IsConnected == true)
            this.connected = true;

        
            if (PhotonNetwork.PlayerList.Length == RoomConfigs.maxRoomPlayers)
            {
                SceneManager.LoadScene(RoomConfigs.CharacterSelectionSceneIndex);
            //PhotonNetwork.LoadLevel(RoomConfigs.CharacterSelectionSceneIndex);

        }
        /*if (PhotonNetwork.PlayerList.Length == RoomConfigs.maxRoomPlayers)
        {
            SceneManager.LoadScene(RoomConfigs.CharacterSelectionSceneIndex);

        }*/
    }








    #region Photon Room Controller 


    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectamos e agora estamos prontos pra primeira Sala");
       //PhotonNetwork.JoinRandomRoom();
    }
    public void CreateRoom(string roomname)
    {
        PhotonNetwork.CreateRoom(roomname);
        this.LobbyCanvas.SetActive(false);
        this.RoomCanvas.SetActive(true);
        Debug.Log("Estamos agora na sala, Jogadores Conectados: " + PlayersConectados);

    }

    public void JoinRoom()
    {
        if (PhotonNetwork.IsConnected == true)
        {
            PhotonNetwork.JoinRandomRoom();
            
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();

        }
    }



    public override void OnJoinedRoom()
    {
        Debug.Log("Entrou na sala");
        if (this.LobbyCanvas.activeInHierarchy)
        {
            this.LobbyCanvas.SetActive (false);
            this.RoomCanvas.SetActive(true);
        }
        if(PhotonNetwork.IsMasterClient)
        {
           // this.StartButton.SetActive(true);
        }
        else 
        {
           // this.StartButton.SetActive(false);
        }
       // PlayersConectados = PhotonNetwork.CurrentRoom.PlayerCount;
        Debug.Log("Player Conectados: " + PlayersConectados);
        //PhotonNetwork.LoadLevel("Aguardando");
        if (PlayersConectados != MaxPlayers)
        {
            Debug.Log("Esperando Players");
        }

        else
        {


            Debug.Log("Pronto Pra Iniciar");

        }
         Player[] players = PhotonNetwork.PlayerList;
         for (int i = 0; i < PhotonNetwork.CurrentRoom.Players.Count(); i++)
         {
             Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        ChangeScene("Menu");
        PlayersConectados--;
    }
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

   private void ChangeScene(string sceneName)
    {
       PhotonNetwork.LoadLevel(sceneName);
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
    }
    #endregion

    #region Some other Methods
    public void salvanomesala(string nomePlaceholder)
    {
        NomeSala.text = nomePlaceholder;

    }

    public void botaocriarsala()
    {
        this.LobbyCanvas.SetActive(false);
        this.PreRoomCanvas.SetActive(true);
    }
    public void voltarButton()
    {
        this.LobbyCanvas.SetActive(true);
        this.PreRoomCanvas.SetActive(false);
    }













    #endregion


}





