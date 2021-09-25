using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject coreCanvas;
    [SerializeField] private KeyCode key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        coreCanvas.SetActive(Input.GetKey(key));
    }
}
