using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpBackgroundColour : MonoBehaviour
{
    [SerializeField] GameManager manager;
    [SerializeField] Camera cam;
    [SerializeField] [Range(0f,1f)] float lerpTime;
    [SerializeField] Color[] myColours;
    int colourIndex = 0;
    float t = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
