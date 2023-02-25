using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class DepthScreen : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private Image screen;
    [SerializeField] private GameObject information;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI depthText;
    [SerializeField] private GameManager gameManager;

    [SerializeField] private float alpha = 0;
    public float timer = 2;
    public bool turningOn = false;
    public bool turningOff = false;

    public void DisplayScreen()
    {
        turningOn = true;
    }

    public void Update()
    {
        var tempColor = screen.color;
        tempColor.a = alpha;
        screen.color = (tempColor);

        if (turningOn == true)
        {
            alpha += Time.deltaTime*1000;
        }

        if (alpha >= 1)
        {
            turningOn = false;
            information.SetActive(true);

            timer -= Time.deltaTime*100;

            if (timer <= 0)
            {
                turningOff = true;
                information.SetActive(false);
            }

        }

        if (turningOff == true)
        {
            alpha -= Time.deltaTime*100;
            if (alpha <= 0)
            {
                turningOff = false;
                gameManager.PauseGame();
            }
        }
    }
}
