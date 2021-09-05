//using Photon.Pun;
//using Photon.Pun.UtilityScripts;
//using Photon.Realtime;
//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class HUDTeamPropsManager : MonoBehaviourPunCallbacks
//{
//    //Canto SUPERIOR ESQUERDO
//    [SerializeField]
//    Image[] orderedBluePlayersCharacter, orderedBluePlayersHeathbar;
//    [SerializeField]
//    TMP_Text orderedBluePlayersReviveTime;

//    //Canto SUPERIOR ESQUERDO
//    [SerializeField]
//    Image[] orderedRedPlayersCharacter, orderedRedPlayersHeathbar;
//    [SerializeField]
//    TMP_Text orderedRedPlayersReviveTime;

//    private Player[] blueTeamMembers;
//    private Player[] redTeamMembers;

//    public Sprite[] characters;

//    private ExitGames.Client.Photon.Hashtable HashProperty = new ExitGames.Client.Photon.Hashtable();


//    // Start is called before the first frame update
//    void Start()
//    {

//        PhotonTeamsManager.Instance.TryGetTeamMembers(1, out blueTeamMembers);
//        PhotonTeamsManager.Instance.TryGetTeamMembers(2, out redTeamMembers);
//        //myTeam = PhotonTeamExtensions.GetPhotonTeam(PhotonNetwork.LocalPlayer);
//        for (int i = 0; i < blueTeamMembers.Length; i++)
//        {
//            //orderedBluePlayersCharacter[i].sprite = blueTeamMembers[i].CustomProperties["characterIcon"];
//            orderedBluePlayersCharacter[i].sprite = characters[i];
//            orderedBluePlayersHeathbar[i].fillAmount = (float)blueTeamMembers[i].CustomProperties["HP"]/(byte)blueTeamMembers[i].CustomProperties["maxHP"];
            
//        }
//        for (int i = 0; i < redTeamMembers.Length; i++)
//        {
//            orderedRedPlayersCharacter[i].sprite = characters[i];
//            orderedRedPlayersHeathbar[i].fillAmount = (float)redTeamMembers[i].CustomProperties["HP"] / (byte)redTeamMembers[i].CustomProperties["maxHP"];
//        }


//    }

//    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
//    {
//        List<Player> TargetblueList = new List<Player>(blueTeamMembers);

//        if(TargetblueList.Contains(targetPlayer))
//        {
//            for (int i = 0; i < blueTeamMembers.Length; i++)
//            {
//                if(blueTeamMembers[i] == targetPlayer)
//                {
//                    orderedBluePlayersHeathbar[i].fillAmount = (float)blueTeamMembers[i].CustomProperties["HP"] / (byte)blueTeamMembers[i].CustomProperties["maxHP"];

//                }
//            }
//        }

//        else
//        {
//            for (int i = 0; i < redTeamMembers.Length; i++)
//            {
//                if (blueTeamMembers[i] == targetPlayer)
//                {
//                    orderedRedPlayersCharacter[i].sprite = characters[i];
//                    orderedRedPlayersHeathbar[i].fillAmount = (float)blueTeamMembers[i].CustomProperties["HP"] / (byte)blueTeamMembers[i].CustomProperties["maxHP"];

//                }
//            }
//        }



//    }

//    private void Update()
//    {
//        if(Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            RandoFillLifebar();
//        }
//    }

//    void RandoFillLifebar()
//    {

//        Player player = blueTeamMembers[Random.Range(1, blueTeamMembers.Length)];
//        int hp = (int)player.CustomProperties["HP"];
//        HashProperty["HP"] = hp - 100;

//    }
 

//}
