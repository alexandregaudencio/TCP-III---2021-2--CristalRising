using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTesteGameplaySarah : MonoBehaviour
{
    public static PlayerTesteGameplaySarah instance ;
    public float Movespeed = 4f;
    private float Turnspeed = 120f;

    public GameObject _objeto1; //Objeto 1
    public GameObject _objeto2; //Objeto 2
    public float _distance; //Armazena a distância
    public bool areaCheck;
    public float points;

    public TMP_Text timeToDisplay;

    // Update is called once per frame
    private void Start()
    {
        instance = this;
        _distance =0;
        areaCheck = false;
        points = 0;
    }
    void Update()
    {
        Controls();
        Distance();
    }
    private void Controls()
    {
        float vert = Input.GetAxis("Vertical");
        float horz = Input.GetAxis("Horizontal");
        this.transform.Translate(Vector3.forward * vert * Movespeed * Time.deltaTime);
        this.transform.localRotation *= Quaternion.AngleAxis(horz * Turnspeed * Time.deltaTime, Vector3.up);
    }
    private void Distance()
    {
        _distance = Vector3.Distance(_objeto1.transform.position, _objeto2.transform.position); //Calculamos a distância e atribuimos a variável
        //Debug.Log(_distance);
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("area"))
        {
            areaCheck = true;
        }

    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("area"))
        {
            areaCheck = false;
        }
    }
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.CompareTag("area"))
        {
            points+=Time.deltaTime;
            //Debug.Log(points);
            string tempTimer = string.Format("{0:00}", points);
            timeToDisplay.text = tempTimer;
        }
    }


}
