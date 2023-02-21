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
    [SerializeField] private List<Depths> levels = new List<Depths>();
    public Depths thisLevel;
    private int levelCounter = 0;
    public DepthScreen depthScreen;
    [SerializeField] private GameObject pausedScreen;

    private bool gamePaused = false;

    // Start is called before the first frame update
    void Start()
    {
        gameStart = true;
        thisLevel = levels[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStart)
        {
            depthMeter += Time.deltaTime * 10;
            DepthText.text = depthMeter.ToString("F0") + "m";
            CheckDepth();
        }
    }

    private void CheckDepth()
    {

        if (depthMeter >= thisLevel.nextLevelTarget)
        {

            levelCounter++;
            thisLevel = levels[levelCounter];
            ChangeLevel();
        }


    }

    public void PauseGame()

    {

        if (gamePaused == false)
        {        
            gamePaused = true;
            pausedScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausedScreen.SetActive(false);
            gamePaused = false;
            Time.timeScale = 1;
        }
    }

    private void ChangeLevel()
    {
        obstacleSpeed = thisLevel.obstacleSpeed;
        depthScreen.DisplayScreen();
        if (ColorUtility.TryParseHtmlString("#" + thisLevel.cameraBackgroundColour, out Color colour))
        {
            cam.backgroundColor = colour;
            RenderSettings.fogColor = colour;
        }
    }
}
