using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class GameplayManager : MonoBehaviourPunCallbacks
{
    [SerializeField] GameObject setUpGameplay;
    public TMP_Text timeToDisplay;
    public static GameplayManager instance;
    private bool endingGame = false;
    private TimerCountdown gameplayRoomTimer;
    public GameObject gameEnd;
    public string msg;
   public Text msgGameEnd;
    [SerializeField] GameObject spawnWallBlue;
    [SerializeField] GameObject spawnWallRed;
    private bool wallDown=false;
    public PhotonView PV;
    private string tempTimer;
    private audioGameplayController audioGameplaySceneScript;
    private void Start()
    {
        gameplayRoomTimer = GetComponent<TimerCountdown>();
        gameplayRoomTimer.CurrentTime = RoomConfigs.instance.heightTime;
        gameplayRoomTimer.BaseTime = RoomConfigs.instance.gameplayTimeBase;
        tempTimer = string.Format("{0:00}", gameplayRoomTimer.BaseTime);
        wallDown = false;
        instance = this;
        gameEnd.SetActive(false);

        audioGameplaySceneScript = GetComponent<audioGameplayController>();

    }

    private void Update()
    {
        UIUpdate();
        audioFortySecondsRemaning();
        if (gameplayRoomTimer.IsBasedownOver() && wallDown == false)
        {
            
            downWallBase();
            gameplayRoomTimer.CurrentTime = RoomConfigs.instance.gameplayMaxTime;
            gameplayRoomTimer.BaseTime = RoomConfigs.instance.heightTime;
            wallDown = true;
            voiceLineStartGame();
        }
            if (gameplayRoomTimer.IsCountdownOver() && wallDown==true )
        {
            if (endingGame) return;
            EndGamebyTimer();
        }
       
    }
    private void audioFortySecondsRemaning()
    {
        if (gameplayRoomTimer.CurrentTime < 41.1f && gameplayRoomTimer.CurrentTime > 40.9f)
        {
            audioGameplaySceneScript.audioGameplayScenePV("gameplayScene");
            audioGameplaySceneScript.audioGameplayScenePV("secondsRemaning");
        }
    }
    private void voiceLineStartGame()
    {
        //GetComponent<audioCharacterSceneController>().audioPlayerVoiceLines("startGame", 1);
        audioGameplaySceneScript.audioPlayerVoiceLines("startGame", 1);

    }
    private void UIUpdate()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PV.RPC("sendTime", RpcTarget.Others, gameplayRoomTimer.CurrentTime);
            PV.RPC("sendBaseTime", RpcTarget.Others, gameplayRoomTimer.BaseTime);
            PV.RPC("SendwallDown", RpcTarget.Others, wallDown);
    }
        if(wallDown)tempTimer = string.Format("{0:00}", gameplayRoomTimer.CurrentTime);
        if(!wallDown)tempTimer = string.Format("{0:00}", gameplayRoomTimer.BaseTime);
        timeToDisplay.text = tempTimer;
    }

    public void EndGamebyTimer()
    {
      
        
        endingGame = true;
        CircleAreaPoints.instance.endingGame = true;
        Debug.Log("Acabou o jogo pelo tempo: ");
       
        if (CircleAreaPoints.instance.pointsTeam1PerCent > CircleAreaPoints.instance.pointsTeam2PerCent) 
        {
            Debug.Log("TEAM1 WINS");
            msg = ("Time is over: TEAM BLUE WINS ");
        }
        else if(CircleAreaPoints.instance.pointsTeam1PerCent < CircleAreaPoints.instance.pointsTeam2PerCent)
        {
            Debug.Log("TEAM2 WINS");
            msg = ("Time is over: TEAM RED WINS ");
        }
        else if(CircleAreaPoints.instance.pointsTeam1PerCent == CircleAreaPoints.instance.pointsTeam2PerCent)
        {
            Debug.Log("EMPATE");
            msg = ("Time is over: EMPATOU ");
        }
        gameEndActive();

    }
    private void downWallBase()
    {
        Destroy(spawnWallBlue);
        Destroy(spawnWallRed);
    }
    public void gameEndActive()
    {
        //Time.timeScale = 0;
        gameEnd.SetActive(true);
        msgGameEnd.text = msg;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.Disconnect();

    }

    public void backToMenu()
    {
        Time.timeScale = 1;
        //Application.Quit();
        SceneManager.LoadScene(RoomConfigs.instance.menuSceneIndex);

    }
    [PunRPC]
    public void sendTime(float time)
    {

        gameplayRoomTimer.CurrentTime = time;


    }
    public void sendBaseTime(float time)
    {

        gameplayRoomTimer.BaseTime = time;


    }
    public void SendwallDown(bool wallDownMaster)
    {

        wallDown= wallDownMaster;


    }
}
