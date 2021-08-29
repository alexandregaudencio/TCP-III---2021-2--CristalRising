using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;



    public class DefineMorte : MonoBehaviour
    {
        public GameObject[] SpawnPointsTimeAzul;
        public GameObject[] SpawnPointTimeVermelho;
        public PlayerProperty PP;
    public float TemporizadorRespawn;
    public bool SpawnCheck;
    public PhotonTeam TimeDesteJogador;

        
    
        [SerializeField] private float avada;

        private void Start()
        {
            PP = this.gameObject.GetComponent<PlayerProperty>();
        TimeDesteJogador = PhotonNetwork.LocalPlayer.GetPhotonTeam();
        }

        private void Update()
        {
            #region Blue Team Spawn
            this.SpawnPointsTimeAzul[0] = GameObject.Find("1B");
            this.SpawnPointsTimeAzul[1] = GameObject.Find("2B");
            this.SpawnPointsTimeAzul[2] = GameObject.Find("3B");
            #endregion

            #region Red Team Spawn
            this.SpawnPointTimeVermelho[0] = GameObject.Find("1A");
            this.SpawnPointTimeVermelho[1] = GameObject.Find("2A");
            this.SpawnPointTimeVermelho[2] = GameObject.Find("3A");
            #endregion
            avada = PP.life;

        if (PP.life <= 0)
        {
            SpawnCheck = true;
        }
        else
            SpawnCheck = false;

        if(SpawnCheck == true)
        {
            this.gameObject.GetComponent<PlayerController>().enabled = false;
           // float T = 0;
            TemporizadorRespawn += Time.deltaTime;
            if(TemporizadorRespawn >= 5.0f)
            {
                Morre();

            }
        }
        else
        {
            TemporizadorRespawn = 0.0f;
            this.gameObject.GetComponent<PlayerController>().enabled = true ;

        }
            
        }
        public void Morre()
        {
        Debug.Log("Ih morri");

        if (TimeDesteJogador.Name == "Blue")
            {
            SpawnCheck = false;
                int R = 0;
                R = Random.Range(0, SpawnPointsTimeAzul.Length);
                GetComponent<Transform>().position = SpawnPointsTimeAzul[R].transform.position;
                GetComponent<Transform>().rotation = SpawnPointsTimeAzul[R].transform.rotation;
                PP.life = 100;
        }
            if (TimeDesteJogador.Name == "Red")
            {
            SpawnCheck = false;
            int W = 0;
                W = UnityEngine.Random.Range(0, SpawnPointsTimeAzul.Length);
                this.gameObject.GetComponent<Transform>().position = SpawnPointTimeVermelho[W].transform.position;
                Debug.Log("Ai calica");
            PP.life = 100;
            }
        
        }
    }
