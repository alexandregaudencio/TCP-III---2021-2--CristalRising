using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float time;
    public float speed;
    private float bufferTime;
    private Vector3 initialPos;
    private bool hit;
    private Camera camera;

    private void Start()
    {
        initialPos = transform.position;
        camera = (Camera)FindObjectOfType(typeof(Camera));
    }

    private void OnEnable()
    {
        bufferTime = time;
        hit = true;
    }
    void Update()
    {
        if (bufferTime > 0)
        {
            transform.localPosition += Vector3.up * Time.deltaTime * speed;
            bufferTime -= Time.deltaTime;
        }
        else
        {
            hit = false;
            Invoke("ResetPosition", 0.1f); 
        }
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
    }

    public void Finish()
    {
        gameObject.SetActive(hit);
    }
    public void ResetPosition() {
        transform.localPosition = initialPos;
    }
}
