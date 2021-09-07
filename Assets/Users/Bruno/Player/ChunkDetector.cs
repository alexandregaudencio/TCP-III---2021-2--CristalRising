using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class ChunkDetector : MonoBehaviour
{
    public const string body = "body";
    public const string head = "head";

    private Collider piece;

    private void Start()
    {
        piece = GetComponent<Collider>();
    }
    public string DetectHit(Collider collider)
    {
        //Debug.Log(collider.transform.position);
        if (piece.bounds.Contains(collider.transform.position) || piece.bounds.Intersects(collider.bounds))
        {
            return gameObject.tag;
        }
        return null;
    }
}
