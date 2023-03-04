using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] public int volume = 100;

    [SerializeField] private Statistics saveStatistics;
    [SerializeField] private InventoryScript saveInventory;
    [SerializeField] private AchivementsManager achivementsManager;
    [SerializeField] private DailyChallengesManager dailyChallengesManager;
    [SerializeField] private BlipScript radar;
    [SerializeField] private TextMeshProUGUI depthText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI experienceText;
    private Color sceneColour;
    //   public DepthScreen depthScreen;
    [SerializeField] private GameObject pausedScreen;

    public int experienceLevel;
    public float experienceFloat;
    public float[] requiredExperience = new float[50];

    public float earntExperience;

    private bool gamePaused = true;

    public void Start()
    {
        sceneColour = cam.backgroundColor;
        LoadMasterFunction();

        saveStatistics.timesLaunched++;
        //  gameStart = true;
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

        PauseGame();
    }

    void Update()
    {
        for (int i = 0; i < 10; i++)
        {
            if (experienceFloat >= requiredExperience[i])
            {
                experienceLevel = i;
            }
        }

        if (gameStart)
        {
            depthMeter += Time.deltaTime * 10;
            DepthText.text = depthMeter.ToString("F0") + "m";
            CheckDepth();
            saveStatistics.playtimeSeconds = saveStatistics.playtimeSeconds + Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }

        cam.backgroundColor = Color.Lerp(cam.backgroundColor, sceneColour, 1f * Time.deltaTime);
        if (radar.activated != true)
        {
            RenderSettings.fogColor = Color.Lerp(cam.backgroundColor, sceneColour, 1f * Time.deltaTime);
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

    public void ReturnToMenu()
    {
        SaveMasterFunction();
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        pausedScreen.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
    }

    public void StartGame()
    {
        pausedScreen.SetActive(false);
        gamePaused = false;
        gameStart = true;
        Time.timeScale = 1;

    }

    public void PauseGame()
    {
        gamePaused = true;
        pausedScreen.SetActive(true);
        Time.timeScale = 0.01f;
    }

    public void TogglePause()
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
        saveStatistics.runs++;
        gameStart = false;
        earntExperience = depthMeter;
        deathScreen.SetActive(true);
        depthText.text = "Depth: " + depthMeter.ToString("0") + "M";
        timeText.text = "Time: " + (depthMeter / 10).ToString("0" + " Seconds");
        experienceText.text = "Experience: " + experienceFloat.ToString("0");
    }

    private void ChangeLevel()
    {
        //  PauseGame();
        obstacleSpeed = thisLevel.obstacleSpeed;
        //   depthScreen.DisplayScreen();
        // depthScreen.depth = depthMeter;
        if (ColorUtility.TryParseHtmlString("#" + thisLevel.cameraBackgroundColour, out Color colour))
        {
            sceneColour = colour;

        }
        RenderSettings.fogDensity = thisLevel.depthDensity;
    }

    public void SaveMasterFunction()
    {
        Debug.Log("GAME SAVED - " + Time.time);
        saveInventory.SaveInventory();
        saveStatistics.saveStats();
        achivementsManager.SaveAchievements();
        // dailyChallengesManager.SaveChallenges();
    }

    public void LoadMasterFunction()
    {
        Debug.Log("GAME LOADED - " + Time.time);
        saveInventory.ReadSave();
        saveStatistics.loadStats();
        achivementsManager.ReadSave();
        // dailyChallengesManager.ReadSave();
    }

    public void ResetMasterFunction()
    {
        Debug.Log("SAVE DELETED - " + Time.time);
        saveInventory.ResetSave();
        saveStatistics.ResetStats();
        achivementsManager.ResetSave();
        dailyChallengesManager.ResetSave();
        // SaveMasterFunction();
    }

    public int GetFishChance(int fishType)
    {
        if (fishType == 1)
        {
            return thisLevel.smallFishChance;
        }
        else
        {
            return thisLevel.mammelChance;
        }

    }
}
