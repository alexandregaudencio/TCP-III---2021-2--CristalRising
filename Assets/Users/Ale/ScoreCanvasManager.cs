using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject coreCanvas;
    [SerializeField] private KeyCode key;


    void Update()
    {
        coreCanvas.SetActive(Input.GetKey(key));
    }
}
