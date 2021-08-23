using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hudtime : MonoBehaviour
{
    public Text aaaaaa;
    void Start()
    {
        aaaaaa.text = GameObject.Find("PlayerAssister").GetComponent<PlayerAssister>().MyTeam.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
