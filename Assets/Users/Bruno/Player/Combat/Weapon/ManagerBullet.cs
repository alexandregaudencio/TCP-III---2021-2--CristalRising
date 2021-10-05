using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerBullet : MonoBehaviour
{
    public Transform[] bulletTransform;
    public Transform current;

    private int count = 0;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            current = bulletTransform[count % bulletTransform.Length];
            count++;
        }
    }
}