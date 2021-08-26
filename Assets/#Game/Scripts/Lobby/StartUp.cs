using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUp : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject CanvasLoadingConnection;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        CanvasLoadingConnection.SetActive(true);
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();

    }

    void Update()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        CanvasLoadingConnection.SetActive(true);
        base.OnDisconnected(cause);
    }

    public override void OnConnectedToMaster()
    {
        CanvasLoadingConnection.SetActive(false);
        base.OnConnectedToMaster();
    }

    //IMPLEENTAR MÉTODO QUE FAZ APARECER O CANVASLOADINGNETWORK QUANDO DESCONECTADO
}
