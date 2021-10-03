using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleGaleria : MonoBehaviour
{
    public GameObject[] nomes;
    public GameObject[] Descricao;
    public void feneco()
    {
        this.nomes[0].SetActive(true);
        this.Descricao[0].SetActive(true);
        for(int R =1; R< nomes.Length; R++)
       {
           nomes[R].SetActive(false);
            Descricao[R].SetActive(false);
       }

    }
    public void Jessie()
    {
        this.nomes[2].SetActive(true);
        this.Descricao[2].SetActive(true);
       
        this.nomes[0].SetActive(false);
        Descricao[0].SetActive(false);

        this.nomes[1].SetActive(false);
        Descricao[1].SetActive(false);

        for (int R = 3; R < nomes.Length; R++)
        {
            nomes[R].SetActive(false);
            Descricao[R].SetActive(false);
        }
        
    }
    public void Susano()
    {
        this.nomes[3].SetActive(true);
        this.nomes[3].SetActive(true);

        this.nomes[0].SetActive(false);
        Descricao[0].SetActive(false);

        this.nomes[1].SetActive(false);
        Descricao[1].SetActive(false);

        this.nomes[2].SetActive(false);
        Descricao[2].SetActive(false);

        for (int R = 4; R < nomes.Length; R++)
        {
            nomes[R].SetActive(false);
            Descricao[R].SetActive(false);
        }
       

    }

    public void Nick()
    {
        this.nomes[1].SetActive(true);
        this.Descricao[1].SetActive(true);

        this.nomes[0].SetActive(false);
        Descricao[0].SetActive(false);

        for (int R = 2; R < nomes.Length; R++)
        {
            nomes[R].SetActive(false);
            Descricao[R].SetActive(false);
        }
    }
    public void Noor()
    {
        this.nomes[5].SetActive(true);
        this.Descricao[5].SetActive(true);
        for(int i = 0; i <= 4 ;i++)
        {
            this.nomes[i].SetActive(false);
            this.Descricao[i].SetActive(false);
        }
    }
    public void Shur()
    {
        this.nomes[4].SetActive(true);
        this.Descricao[4].SetActive(true);

        this.nomes[0].SetActive(false);
        Descricao[0].SetActive(false);

        this.nomes[1].SetActive(false);
        Descricao[1].SetActive(false);

        this.nomes[2].SetActive(false);
        Descricao[2].SetActive(false);
        
        this.nomes[3].SetActive(false);
        this.nomes[3].SetActive(false);

        this.nomes[5].SetActive(false);
        this.Descricao[5].SetActive(false);
        

       
    }
}
