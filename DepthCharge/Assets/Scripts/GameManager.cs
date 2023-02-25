using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text DepthText;
    [SerializeField] private Camera cam;
    private float depthMeter;
    public bool gameStart;
    private float obstacleSpeed;
    [SerializeField] private List<Depths> levels = new List<Depths>();
    public Depths thisLevel;
    private int levelCounter = 0;
    [SerializeField] private GameObject deathScreen;

    [SerializeField] private Statistics saveStatistics;
    [SerializeField] private InventoryScript saveInventory;

    public DepthScreen depthScreen;
    [SerializeField] private GameObject pausedScreen;

    public int experienceLevel;
    public float experienceFloat;
    public float[] requiredExperience = new float[50];

    public float earntExperience;

    private bool gamePaused = false;

    void Start()
    {
        gameStart = true;
        thisLevel = levels[0];

        requiredExperience[1] = 10;
        requiredExperience[2] = 100;
        requiredExperience[3] = 250;
        requiredExperience[4] = 500;
        requiredExperience[5] = 1000;
        requiredExperience[6] = 2000;
        requiredExperience[7] = 4000;
        requiredExperience[8] = 7500;
        requiredExperience[9] = 12000;
        requiredExperience[10] = 20000;

        LoadMasterFunction();
    }

    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if(experienceFloat >= requiredExperience[i])
            {
                experienceLevel = i;
            }
        }

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
            Time.timeScale = 0.01f;
        }
        else
        {
            pausedScreen.SetActive(false);
            gamePaused = false;
            Time.timeScale = 1;
        }
    }

    public void EndGame()
    {

        gameStart = false;
        earntExperience = depthMeter;
        deathScreen.SetActive(true);
    }

    private void ChangeLevel()
    {
        PauseGame();
        obstacleSpeed = thisLevel.obstacleSpeed;
        depthScreen.DisplayScreen();
        if (ColorUtility.TryParseHtmlString("#" + thisLevel.cameraBackgroundColour, out Color colour))
        {
            cam.backgroundColor = colour;
            RenderSettings.fogColor = colour;
        }
    }

    public void SaveMasterFunction()
    {
        saveInventory.SaveInventory();
        saveStatistics.saveStats();
    }

    public void LoadMasterFunction()
    {
        saveInventory.ReadSave();
        saveStatistics.loadStats();
    }
}
