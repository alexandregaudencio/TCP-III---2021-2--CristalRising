using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSM : MonoBehaviour
{
    
    void Start()
    {
        
    }


    void Update()
    {
        if(GameObject.Find("SceneController").GetComponent<TimerCountdown>().CurrentTime <=0)
        {
            int RandomChooser = Random.Range(0,3);
            if (RandomChooser == 1)
            {
                Choose1();
            }
            else if(RandomChooser==2)
            {
                Choose2();
            }
            else if(RandomChooser == 3)
            {
                Choose3();
            }
            
        }
    }

    public void Choose1()
    {
        GameObject.Find("PlayerAssister").GetComponent<PlayerAssister>().Choose1();
    }

    public void Choose2()
    {
        GameObject.Find("PlayerAssister").GetComponent<PlayerAssister>().Choose2();
    }


    public void Choose3()
    {
        GameObject.Find("PlayerAssister").GetComponent<PlayerAssister>().Choose3();
    }
}
