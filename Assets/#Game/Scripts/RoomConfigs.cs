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
    public int maxRoomPlayers;

    //characterSelection
    public int characterSelectionMaxTime;

    //Gameplay
    public int gameplayMaxTime;
    public int heightTime;
    public int gameplayTimeBase;

    //SceneIndex
    public int menuSceneIndex;
    public int CharSelecSceneIndex;
    public int gameplaySceneIndex;


    //Players
    public int timeToRespawn;

    //team
    public Color blueTeamColor = new Color(0, 129, 255, 255);
    public Color redTeamColor = new Color(233, 36, 41, 255);


    public Character[] charactersOrdered;
}
