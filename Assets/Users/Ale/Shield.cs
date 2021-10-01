using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Spells, IEffect
{
    public float timer;
    public int health;

    public void Apply(Animator animatorId = null)
    {
        GetComponentInChildren<GameObject>().SetActive(true);        
    }

    public override void BuildElement()
    {
        //Se fosse uma habilidade de alterar a propriedade de um personagem;
        //GetComponentInParent<PlayerProperty>().MoveSpeed = attribute.speed;
    }

    public override void Aim()
    {
        //Se fosse fazer o escudo esférico?
        //int mask = LayerMask.GetMask(LayerMask.LayerToName(transform.parent.gameObject.layer));

        //RaycastHit hit;
        //if (Physics.Raycast(Camera.main.ScreenPointToRay().origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction, out hit, 10000, mask))
        //{
        //    hit.collider.gameObject.GetComponent</*EscudoEsferico*/>.use();
        //}


    }


    public override void Use()
    {
        Apply();
        GetComponent<BoxCollider>().enabled = true;
        BuildElement();
        Aim();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("hit"))
        {
            health -= collision.gameObject.GetComponent<Bullet>().damage;
            if (health <= 0)
            {
                GetComponentInChildren<GameObject>().SetActive(true);
                GetComponent<BoxCollider>().enabled = true;
            }
        }
    }


}
