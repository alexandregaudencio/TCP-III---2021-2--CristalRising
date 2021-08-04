using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetUp : MonoBehaviourPun
{
    public static GameSetUp GS;
    public Transform[] spawnPoints;

    private void OnEnable()
    {
        if (GameSetUp.GS == null) 
        {
            GameSetUp.GS = this;

        }
    }
}
