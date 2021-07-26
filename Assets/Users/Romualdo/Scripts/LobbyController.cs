using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine.UI;

public class LobbyController: MonoBehaviourPunCallbacks
{
    public byte PlayersConectados, MaxPlayers;
    public static LobbyController instance;
    public GameObject LobbyCanvas;
    public GameObject RoomCanvas;
    public Text NumeroJogadores;
    public bool connected;
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
        if (PhotonNetwork.IsConnected == true)
            this.connected = true;
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
        PlayersConectados = PhotonNetwork.CurrentRoom.PlayerCount;
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
    }
    public override void OnDisconnected(DisconnectCause cause)
    {
        ChangeScene("Menu");
        PlayersConectados--;
    }
    public void QuitRoom()
    {
        PhotonNetwork.LeaveRoom();
        this.RoomCanvas.SetActive(false);
        this.LobbyCanvas.SetActive(true);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("Um player desconectou");
    }

    private void ChangeScene(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
    #endregion
}





