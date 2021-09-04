using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCountdown : MonoBehaviour
{
    float maxTime;
    float currentTime;
    float timePassed;

    public float CurrentTime { get => currentTime; set => currentTime = value; }
   
    public bool IsCountdownOver()
    {
        return (CurrentTime > 0.0f) ? false : true;
    }
    public bool IsCountdownStart()
    {
       timePassed = RoomConfigs.instance.gameplayMaxTime - CurrentTime ;
        return (timePassed < 5) ? false : true;
    }

    private void FixedUpdate()
    {
        if (CurrentTime >= 0) CurrentTime -= Time.fixedDeltaTime;
    }

}
