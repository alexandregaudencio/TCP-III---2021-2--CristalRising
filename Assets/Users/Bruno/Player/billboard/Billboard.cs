using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera camera;
    void Start()
    {
        camera = (Camera)FindObjectOfType(typeof(Camera));
    }

    void Update()
    {
        transform.LookAt(transform.position - camera.transform.forward);
    }
}
