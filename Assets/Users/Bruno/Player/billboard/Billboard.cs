using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private new Camera camera;
    void Start()
    {
        camera = (Camera)FindObjectOfType(typeof(Camera));
    }

    void Update()
    {
        transform.LookAt(transform.position - camera.transform.forward);

    }
    private void LateUpdate()
    {
        camera = (Camera)FindObjectOfType(typeof(Camera));
    }
}
