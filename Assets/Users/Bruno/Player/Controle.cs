using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle : MonoBehaviour
{
    private new Transform transform;
    public float speed;
    public Weapon gun;
    public Spells spell;
    public Animator animator;
    private GameObject aux;
    void Start()
    {
        this.transform = GetComponent<Transform>();
        if (aux)
            aux = Instantiate(animator.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gun.Use();
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (spell)
            {
                (spell as IActive).Aim();
                (spell as IEffect).Apply(aux.GetComponent<Animator>());
            }
        }
    }
}
