using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text DepthText;
    [SerializeField] private Camera cam;
    private float depthMeter;
    private bool gameStart;
    private float obstacleSpeed;

  

    // Start is called before the first frame update
    void Start()
    {
        gameStart = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            depthMeter += Time.deltaTime * 10;
            DepthText.text = depthMeter.ToString("F0") + "m";
                 
        }
    }

    private void CheckDepth()
    {
        if(depthMeter>= 200 && depthMeter<= 400)
        {

        }
    }
}
