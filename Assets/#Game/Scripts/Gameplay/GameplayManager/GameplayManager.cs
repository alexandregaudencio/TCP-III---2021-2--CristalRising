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
    private void Start()
    {
        gameplayRoomTimer = GetComponent<TimerCountdown>();
        gameplayRoomTimer.CurrentTime = RoomConfigs.gameplayTimeBase;
       // gameplayRoomTimer.CurrentTime = RoomConfigs.gameplayMaxTime;
        instance = this;
        gameEnd.SetActive(false);
    }

    private void Update()
    {
        UIUpdate();

        if (gameplayRoomTimer.IsCountdownOver())
        {
            if (wallDown == false)
            {
                downWallBase();
                gameplayRoomTimer.CurrentTime = RoomConfigs.gameplayMaxTime;
                wallDown = true;

            }

            if (gameplayRoomTimer.IsCountdownStart())
            {
                if (wallDown == true)
                {
                    if (endingGame) return;
                    EndGamebyTimer();
                }
            }
        }
    }

    private void UIUpdate()
    {
        string tempTimer = string.Format("{0:00}", gameplayRoomTimer.CurrentTime);
        timeToDisplay.text = tempTimer;
    }

    public void EndGamebyTimer()
    {
      
        
        endingGame = true;
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
        Time.timeScale = 0;
        gameEnd.SetActive(true);
        msgGameEnd.text = msg;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PhotonNetwork.LeaveRoom();

    }

    public void backToMenu()
    {
        Time.timeScale = 1;
        //Application.Quit();
        SceneManager.LoadScene(RoomConfigs.menuSceneIndex);

    }

}
