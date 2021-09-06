using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = (Camera)FindObjectOfType(typeof(Camera));
    }

    void Update()
    {
        //transform.LookAt(camera.transform.position, Vector3.up);
        //Vector3 looAtTarget = transform.position + camera.transform.forward;

        transform.LookAt(transform.position - cam.transform.forward);

    }
    private void LateUpdate()
    {
        cam = (Camera)FindObjectOfType(typeof(Camera));
    }
}
