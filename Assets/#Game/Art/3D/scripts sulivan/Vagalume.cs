using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vagalume : MonoBehaviour
{

    public GameObject objeto;
   
    Movimento movimento;

    public float speed;
    public float tempo;
    public float tempoFinal;


    void Start()
    {

        movimento = new Movimento(objeto, objeto.transform.position, speed, tempo, tempoFinal);

    }

    
    void Update()
    {

        movimento.Update(Time.deltaTime);


    }




}
