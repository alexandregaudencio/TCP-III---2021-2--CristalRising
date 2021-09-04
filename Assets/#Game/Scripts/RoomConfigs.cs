using UnityEngine;

public static class RoomConfigs
{
    //CreateRoomC
    public const int maxRoomPlayers = 6;
    public const int maxPlayersTeam = maxRoomPlayers / 2;

    //characterSelection
    public const int characterSelectionMaxTime = 5;

    //Gameplay
    public const int gameplayMaxTime = 100;
    public const int gameplayTimeBase = 10;

    //SceneIndex
    public const int menuSceneIndex = 0;
    public const int CharSelecSceneIndex = 1;
    public const int gameplaySceneIndex = 2;


    //Players
    public const int timeToRespawn = 8;

    //team
    public static Color blueTeamColor = new Color(0, 129, 255);
    public static Color redTeamColor = new Color(233, 36 , 41);

}
