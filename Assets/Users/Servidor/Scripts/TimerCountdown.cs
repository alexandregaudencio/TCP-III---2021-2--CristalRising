using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerCountdown : MonoBehaviour
{
    float maxTime;
    float currentTime;

    public float CurrentTime { get => currentTime; set => currentTime = value; }

    public bool IsCountdownOver()
    {
        return (CurrentTime > 0.0f) ? false : true;
    }

    private void FixedUpdate()
    {
        if (CurrentTime >= 0) CurrentTime -= Time.fixedDeltaTime;
    }

}
