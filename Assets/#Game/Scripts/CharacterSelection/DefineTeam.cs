
using Photon.Pun.UtilityScripts;
using Photon.Pun;


public class DefineTeam : PhotonTeamsManager
{

    public void TeamDefinition()
    {
        for (int i = 0; i < PhotonNetwork.CurrentRoom.PlayerCount; i++)
        {
            if(i%2 == 0) PhotonNetwork.PlayerList[i].JoinTeam("Blue");
            else         PhotonNetwork.PlayerList[i].JoinTeam("Red");
        }
    }

}
