using System.Collections;
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
    private void Start()
    {
        gameplayRoomTimer = GetComponent<TimerCountdown>();
        gameplayRoomTimer.CurrentTime = RoomConfigs.gameplayMaxTime;
        instance = this;
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
       // Time.timeScale = 0;
        endingGame = true;
        Debug.Log("Acabou o jogo pelo tempo: ");
        if (CircleAreaPoints.instance.pointsTeam1PerCent > CircleAreaPoints.instance.pointsTeam2PerCent) 
        {
            Debug.Log("TEAM1 WINS");
        }
        else if(CircleAreaPoints.instance.pointsTeam1PerCent < CircleAreaPoints.instance.pointsTeam2PerCent)
        {
            Debug.Log("TEAM2 WINS");
        }
        else if(CircleAreaPoints.instance.pointsTeam1PerCent == CircleAreaPoints.instance.pointsTeam2PerCent)
        {
            Debug.Log("EMPATE");
        }

       

    }

   
}
