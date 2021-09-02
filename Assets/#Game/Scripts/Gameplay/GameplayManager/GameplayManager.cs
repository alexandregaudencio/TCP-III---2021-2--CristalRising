using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;
using Photon.Pun.UtilityScripts;

public class GameplayManager : MonoBehaviourPunCallbacks
{
    public TMP_Text timeToDisplay;
    public static GameplayManager instance;
    private bool endingGame = false;
    private TimerCountdown gameplayRoomTimer;
    public GameObject gameEnd;
    public string msg;
   public Text msgGameEnd;
    int timer = 999;

    private void Awake()
    {

    }
    private void Start()
    {
        //gameplayRoomTimer = GetComponent<TimerCountdown>();
        //gameplayRoomTimer.CurrentTime = RoomConfigs.gameplayMaxTime;
        instance = this;
        gameEnd.SetActive(false);
    }

    private void Update()
    {
        CountdownTimer.TryGetStartTime(out timer);



        UIUpdate();

        if (gameplayRoomTimer.IsCountdownOver())
        {
            if (endingGame) return;
            EndGamebyTimer();
        }
    }

    private void UIUpdate()
    {
        //string tempTimer = string.Format("{0:00}",CountdownTimer);
        //timeToDisplay.text = tempTimer;
        //timeToDisplay.text = CountdownTimer.text;
        
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
