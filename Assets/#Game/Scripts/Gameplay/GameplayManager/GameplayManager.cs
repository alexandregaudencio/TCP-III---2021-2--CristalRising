﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.Linq;

public class GameplayManager : MonoBehaviour
{
    public TMP_Text timeToDisplay;
    public static GameplayManager instance;
    private bool endingGame = false;
    private TimerCountdown gameplayRoomTimer;
    public GameObject gameEnd;
    public string msg;
   public Text msgGameEnd;
    private void Start()
    {
        gameplayRoomTimer = GetComponent<TimerCountdown>();
        gameplayRoomTimer.CurrentTime = RoomConfigs.gameplayMaxTime;
        instance = this;
        gameEnd.SetActive(false);
    }

    private void Update()
    {
        UIUpdate();

        if (gameplayRoomTimer.IsCountdownOver())
        {
            if (endingGame) return;
            EndGamebyTimer();
        }
    }

    private void UIUpdate()
    {
        string tempTimer = string.Format("{0:00}", gameplayRoomTimer.CurrentTime);
        timeToDisplay.text = tempTimer;
    }

    public void EndGamebyTimer()
    {
       Time.timeScale = 0;
        
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
        gameEnd.SetActive(true);
        msgGameEnd.text = msg;

    }

   public void backToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(RoomConfigs.menuSceneIndex);
    }
}
