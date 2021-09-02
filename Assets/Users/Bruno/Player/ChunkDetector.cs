using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ChunkDetector : MonoBehaviour
{
    private Collider piece;
    private void Start()
    {
        piece = GetComponent<Collider>();
    }
    public void DetectHit(Collider collider)
    {
        Debug.Log("detect hit");
        if (piece.bounds.Intersects(collider.bounds))
        {
            Debug.Log("acertou " + name);
        }
    }
}
