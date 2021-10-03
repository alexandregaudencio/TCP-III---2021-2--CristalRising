using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento 
{
    public enum STATE
    {
        X,
        Z,
    }


    STATE state;
    int P = 1;
    GameObject objeto;
    Vector3 posicao;
    float speed;
    float tempo;
    float tempoFinal;
    int dir = 1;


    public Movimento(GameObject objeto, Vector3 posicao, float speed, float tempo, float tempoFinal)
    {
        this.objeto = objeto;
        this.speed = speed;
        this.tempo = tempo;
        this.tempoFinal = tempoFinal;
        this.posicao = posicao;


    }





    void Start()
    {
        
    }

    
    public void Update(float gameTime)
    {

        atualizandoPosicao(gameTime);
        this.tempo += 1;

        if (this.tempo >= this.tempoFinal)
        {
            this.tempo = 0;
            escolhendo();
            posicaoObjeto();


        }

        this.objeto.transform.position = this.posicao;



    }


    private void atualizandoPosicao(float gameTime)
    {

        switch (state)
        {
            case STATE.X:
                this.posicao += new Vector3(dir * this.speed * gameTime, 0, 0);
                break;
            case STATE.Z:
                this.posicao += new Vector3(0, 0, dir * this.speed * gameTime);
                break;

        }


    }

    private void escolhendo()
    {
        int r = Random.Range(1, 3);

        switch (state)
        {

            case STATE.X:

                switch (r)
                {
                    case 1:
                        this.state = STATE.Z;
                        break;
                    case 2:
                        this.state = STATE.X;
                        break;

                }


                break;
            case STATE.Z:

                switch (r)
                {
                    case 1:

                        this.state = STATE.Z;
                        break;
                    case 2:

                        this.state = STATE.X;
                        break;

                }


                break;

        }



    }

    private void posicaoObjeto()
    {

        switch (P)
        {
            case 1:
                switch (state)
                {
                    case STATE.X:
                        this.dir = 1;
                        this.P = 2;
                        this.objeto.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case STATE.Z:
                        this.dir = -1;
                        this.P = 4;
                        this.objeto.transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                }
                break;
            case 2:
                switch (state)
                {
                    case STATE.X:
                        this.dir = -1;
                        this.objeto.transform.rotation = Quaternion.Euler(0, -90, 0);
                        this.P = 1;
                        break;
                    case STATE.Z:
                        this.dir = -1;
                        this.P = 3;
                        this.objeto.transform.rotation = Quaternion.Euler(0, 180, 0);
                        break;
                }
                break;
            case 3:
                switch (state)
                {
                    case STATE.X:
                        this.dir = -1;
                        this.P = 4;
                        this.objeto.transform.rotation = Quaternion.Euler(0, -90, 0);
                        break;
                    case STATE.Z:
                        this.dir = 1;
                        this.P = 2;
                        this.objeto.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                }
                break;
            case 4:
                switch (state)
                {
                    case STATE.X:
                        this.dir = 1;
                        this.P = 3;
                        this.objeto.transform.rotation = Quaternion.Euler(0, 90, 0);
                        break;
                    case STATE.Z:
                        this.dir = 1;
                        this.P = 1;
                        this.objeto.transform.rotation = Quaternion.Euler(0, 0, 0);
                        break;
                }
                break;

        }
    }






}
