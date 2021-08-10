using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CircleAreaPoints : MonoBehaviour
{
    public static CircleAreaPoints instance;
    public float pointsTeam1;
    public float pointsTeam2;

    [SerializeField] int maxPoints;

    [SerializeField] Image pointsBarImageTeam1;
    [SerializeField] Image pointsBarImageTeam2;

    [SerializeField] TMP_Text pointsUiTeam1;
    [SerializeField] TMP_Text pointsUiTeam2;
   
    [SerializeField] int constPoint;

    [SerializeField] PlayerTesteGameplaySarah[] Team1;
    [SerializeField] PlayerTesteGameplaySarah[] Team2;
    private int countPlayerinAreaTeam1;
    private int countPlayerinAreaTeam2;

    private int countPlayerExtraTeam1;
    private int countPlayerExtraTeam2;

    private bool endingGame = false;
    void Start()
    {
        pointsTeam1 = 0;
        pointsTeam2 = 0;
        countPlayerinAreaTeam1 = 0;
        countPlayerinAreaTeam2 = 0;
        instance = this;


    }

    // Update is called once per frame
    void Update()
    {
        AreaCheck();
        PointsControl();

        

    }
    private void AreaCheck()
    {
        /*if (PlayerTesteGameplaySarah.instance._distance <= 3)
        {
            Debug.Log("Estamos dentro da area");
        }
        else
        {
            Debug.Log("Estamos fora da area");
        }
        
        if (PlayerTesteGameplaySarah.instance.areaCheck == true)
        {
            Debug.Log("Estamos dentro da area");
        }
        else
        {
            Debug.Log("Estamos fora da area");
        }
       */
    }
    private void PointsControl()
    {
        countPlayerinAreaTeam1 = 0;
        countPlayerinAreaTeam2 = 0;
        pointsBarImageTeam1.fillAmount = pointsTeam1 / maxPoints;
        pointsBarImageTeam2.fillAmount = pointsTeam2 / maxPoints;

        for (int i = 0; i < Team1.Length; i++)
            {
                if (Team1[i].colissionTeam1 == true)
                {
                countPlayerinAreaTeam1++;
                }
            }
            for (int i = 0; i < Team2.Length; i++)
            {
                if (Team2[i].colissionTeam2 == true)
                {
                countPlayerinAreaTeam2++;
                }
            }

           if(countPlayerinAreaTeam1 != countPlayerinAreaTeam2)
            {
            if (pointsTeam1 < 2999 && pointsTeam2 < 2999)
            {
                int diffPlayerinArea = countPlayerinAreaTeam1- countPlayerinAreaTeam2;
                if (diffPlayerinArea > 0)
                {
                    countPlayerExtraTeam1 = diffPlayerinArea;
                    countPlayerExtraTeam2 = 0;
                }
                else if (diffPlayerinArea < 0)
                {
                    countPlayerExtraTeam2 = -diffPlayerinArea;
                    countPlayerExtraTeam1 = 0;
                }
                else if (diffPlayerinArea == 0)
                {
                    countPlayerExtraTeam2 = 0;
                    countPlayerExtraTeam1 = 0;
                }

                pointsTeam1 = pointsTeam1 + constPoint * Time.deltaTime * countPlayerExtraTeam1;
                string pointStringTeam1 = string.Format("{0:00}", pointsTeam1);
                pointsUiTeam1.text = pointStringTeam1;

                #region teste aparecer apenas de 50 em 50
                //float resultado = pointsTeam1 % 50;
                //Debug.Log(resultado);
                /*if (resultado>-1.5 && resultado<1.5)
                {
                    pointsUiTeam1.text = pointStringTeam1;
                }
                   */

                #endregion

                pointsTeam2 = pointsTeam2 + constPoint * Time.deltaTime * countPlayerExtraTeam2;
                string pointStringTeam2 = string.Format("{0:00}", pointsTeam2);
                pointsUiTeam2.text = pointStringTeam2;
            }
            else
                 {
                    if (endingGame) return;
                    EndGamebyPoints();
                } 
        }
            
     
    }
    private void EndGamebyPoints()
    {
        
       endingGame = true;
        Debug.Log("GAME ENDS: ");
        if (pointsTeam1 >pointsTeam2) Debug.Log("TEAM1 WINS");
        if (pointsTeam2 >pointsTeam1) Debug.Log("TEAM2 WINS");
        if (pointsTeam2 ==pointsTeam1) Debug.Log("EMPATE");
        Time.timeScale = 0;
    }
  
}
