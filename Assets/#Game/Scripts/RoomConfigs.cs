using System.Collections.Generic;
using UnityEngine;

public  class RoomConfigs : MonoBehaviour
{
    public static RoomConfigs instance;

    private void Start()
    {
        instance = this;
    }

    //CreateRoomC
    public int maxRoomPlayers = 6;

    //characterSelection
    public int characterSelectionMaxTime = 5;

    //Gameplay
    public int gameplayMaxTime = 100;
    public int heightTime = 1000;
    public int gameplayTimeBase = 10;

    //SceneIndex
    public int menuSceneIndex = 0;
    public int CharSelecSceneIndex = 1;
    public int gameplaySceneIndex = 2;


    //Players
    public int timeToRespawn = 8;

    //team
    public Color blueTeamColor = new Color(0, 129, 255, 255);
    public Color redTeamColor = new Color(233, 36, 41, 255);


    public Character[] charactersOrdered;
}
