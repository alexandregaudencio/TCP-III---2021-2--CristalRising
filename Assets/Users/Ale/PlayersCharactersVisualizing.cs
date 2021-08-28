using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Photon.Pun;

public class PlayersCharactersVisualizing : MonoBehaviourPunCallbacks
{

    [SerializeField] private Image[] PlayersCharacterIcon;
    [SerializeField] private TMP_Text[] PlayersTextName;

    private Player[] myTeamMembers;

    // Start is called before the first frame update
    void Start()
    {
        PhotonTeam myTeam = PhotonTeamExtensions.GetPhotonTeam(PhotonNetwork.LocalPlayer);

        PhotonTeamsManager.Instance.TryGetTeamMembers(myTeam.Code, out myTeamMembers);

        for (int i = 0; i < myTeamMembers.Length; i++)
        {
            PlayersTextName[i].text =  myTeamMembers[i].NickName;
        }
  

    }
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerChoosing(GameObject CharacterPrefab)
    {

    }



}

