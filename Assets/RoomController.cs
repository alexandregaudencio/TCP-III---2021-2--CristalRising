using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RoomController : MonoBehaviourPunCallbacks, IInRoomCallbacks
{
    public static RoomController room;
    public GameObject StartButton;
    public Transform playersPanel;
    public GameObject playerListingPrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        Debug.Log("Estamos em uma sala agora");

       
        if (PhotonNetwork.IsMasterClient)
        {
            StartButton.SetActive(true);
        }

   
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        Debug.Log("Novo jogador entrou na sala");
        ClearPlayerListings();
        ListPlayers();
    }

    void ClearPlayerListings()
    {
        for (int i = playersPanel.childCount - 1; i >= 0; i--)
        {
            Destroy(playersPanel.GetChild(i).gameObject);
        }
    }

    void ListPlayers()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                GameObject tempListing = Instantiate(playerListingPrefab, playersPanel);
                Text tempText = tempListing.transform.GetChild(0).GetComponent<Text>();
                tempText.text = player.NickName;
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.LogError(otherPlayer.NickName + " saiu da sala");
        ClearPlayerListings();
        ListPlayers();
    }
}
