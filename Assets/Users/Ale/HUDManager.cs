using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{

    //Canto SUPERIOR ESQUERDO
    [SerializeField]
    Image[] ordenedBluePlayersCharacter, ordenedBluePlayersHeathbar;
    [SerializeField]
    TMP_Text ordenedBluePlayersReviveTime;

    //Canto SUPERIOR ESQUERDO
    [SerializeField]
    Image[] ordenedRedPlayersCharacter, ordenedRedPlayersHeathbar;
    [SerializeField]
    TMP_Text ordenedREdPlayersReviveTime;

    //Centro SUPERIOR
    [SerializeField] TMP_Text timer, blueTeamScore, redTeamScore;
    [SerializeField] Image blueScoreBar, redScoreBar;

    //Centro CENTRO
    [SerializeField] GameObject crossImg;
    [SerializeField] TMP_Text ammoText;

    //Canto INFERIOR ESQUERDO
    [SerializeField] Image healthBar;
    [SerializeField] TMP_Text healthText, playerCharacterName;
    [SerializeField] Image playerCharacterIcon;


    //Canto INFERIOR DIREITO



}
