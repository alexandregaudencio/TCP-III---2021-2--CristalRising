using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

public class CircleAreaPoints : MonoBehaviour
{
    public static CircleAreaPoints instance;
    public float pointsTeam1;
    public float pointsTeam2;
    public PhotonView PV;

    [SerializeField] int maxPoints;

    [SerializeField] Image pointsBarImageTeam1;
    [SerializeField] Image pointsBarImageTeam2;

    [SerializeField] TMP_Text pointsUiTeam1;
    [SerializeField] TMP_Text pointsUiTeam2;
   
    [SerializeField] int constPoint;

  
    public int countPlayerinAreaTeam1;
    public int countPlayerinAreaTeam2;

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
    void FixedUpdate()
    {
        PointsControl();

    }
 
    private void PointsControl()
    {
        pointsBarImageTeam1.fillAmount = pointsTeam1 / maxPoints;
        pointsBarImageTeam2.fillAmount = pointsTeam2 / maxPoints;

     
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

                
                string pointStringTeam1 = string.Format("{0:00}", pointsTeam1);
                pointsUiTeam1.text = pointStringTeam1;


                
                string pointStringTeam2 = string.Format("{0:00}", pointsTeam2);
                pointsUiTeam2.text = pointStringTeam2;


                if (PhotonNetwork.IsMasterClient)
                {
                    pointsTeam1 = pointsTeam1 + constPoint * Time.fixedDeltaTime * countPlayerExtraTeam1;
                    pointsTeam2 = pointsTeam2 + constPoint * Time.fixedDeltaTime * countPlayerExtraTeam2;
                    PV.RPC("sendPoints", RpcTarget.Others, pointsTeam1, pointsTeam2);
                }


            }
            else
                 {
                    if (endingGame) return;
                    EndGamebyPoints();
                } 
        }
            
     
    }
    [PunRPC]
    public void RPC_sendIncrease(string team)
    {
        if(team=="Blue")countPlayerinAreaTeam1++;
        if(team=="Red")countPlayerinAreaTeam2++;
    }
    [PunRPC]
    public void RPC_sendDecrease(string team) 
    {
        if (team == "Blue") this.countPlayerinAreaTeam1--;
        if (team == "Red") this.countPlayerinAreaTeam2--;
    }

    [PunRPC]
    public void sendPoints(float teamPontoB, float teamPontoR)
    {
        
        pointsTeam1 = teamPontoB;
        pointsTeam2 = teamPontoR;

       
    }



    private void EndGamebyPoints()
    {
        
       endingGame = true;
        Debug.Log("GAME ENDS: ");
        if (pointsTeam1 >pointsTeam2) Debug.Log("TEAM1 WINS");
        if (pointsTeam2 >pointsTeam1) Debug.Log("TEAM2 WINS");
        if (pointsTeam2 ==pointsTeam1) Debug.Log("EMPATE");
        //Time.timeScale = 0;
    }


    void OnTriggerEnter(Collider collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.gameObject.layer==8)//team1
             {
                PV.RPC("RPC_sendIncrease", RpcTarget.All, "Blue");
            }
            if (collision.gameObject.layer == 9)//team2
            {
                PV.RPC("RPC_sendIncrease", RpcTarget.All, "Red");
            }
         }

    }


    void OnTriggerExit(Collider collision)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (collision.gameObject.layer == 8)//team1
            {
                PV.RPC("RPC_sendDecrease", RpcTarget.All, "Blue");
            }
            if (collision.gameObject.layer == 9)//team2
            {
                PV.RPC("RPC_sendDecrease", RpcTarget.All, "Red");
            }
        }

    }



}
