﻿using System.Collections;
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
        aux = Instantiate(animator.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("A"))
        {
            transform.Rotate(Vector3.up * -speed * 20 * Time.deltaTime);
        }
        if (Input.GetButton("D"))
        {
            transform.Rotate(Vector3.up * speed * 20 * Time.deltaTime);
        }
        if (Input.GetButton("S"))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (Input.GetButton("W"))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if (Input.GetMouseButtonDown(0))
        {
            gun.Use();
        }
        if (Input.GetMouseButtonDown(1))
        {
            (spell as IActive).Aim();
            (spell as IEffect).Apply(aux.GetComponent<Animator>());
        }
    }
}
