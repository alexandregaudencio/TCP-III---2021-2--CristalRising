using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAreaPoints : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AreaCheck();

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
        */
        if (PlayerTesteGameplaySarah.instance.areaCheck == true)
        {
            //Debug.Log("Estamos dentro da area");
        }
        else
        {
           // Debug.Log("Estamos fora da area");
        }
    }
}
