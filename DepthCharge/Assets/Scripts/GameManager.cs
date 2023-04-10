using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager currentManager;

    private void Awake()
    {
        currentManager = this;       
        Invoke("LoadMasterFunction",0.01f); 
    }

    #region Variables
    [SerializeField] private Camera cam;

    //Depth values and levels
    [SerializeField] private TMP_Text DepthText;
    [SerializeField] private TextMeshProUGUI depthText;
    private float depthMeter;
    [SerializeField] private List<Depths> levels = new List<Depths>();
    public Depths thisLevel;
    private int levelCounter = 0;

    public bool gameStart;

    [SerializeField] private GameObject deathScreen;
    [SerializeField] public int volume = 100;
    //save states
    [SerializeField] private Statistics saveStatistics;
    [SerializeField] private InventoryScript saveInventory;
    //challenges and achievements
    [SerializeField] private AchivementsManager achivementsManager;
    [SerializeField] private DailyChallengesManager dailyChallengesManager;

    [SerializeField] private BlipScript radar;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI experienceText;
    private Color sceneColour;
    [SerializeField] private GameObject pausedScreen;

    public int experienceLevel;
    public float experienceFloat;
    public float[] requiredExperience = new float[50];

    public float earntExperience;

    private bool gamePaused;

    #endregion


    public void Start()
    {     
        Time.timeScale = 0.01f; //Pause time
        sceneColour = cam.backgroundColor;      //scene colour is the cameras background
        saveStatistics.timesLaunched++; //Add to the amount of times launched.
        saveStatistics.saveStats(); 
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

        gameStart = false;
        gamePaused = true;
        pausedScreen.SetActive(true);

    }

    void Update()
    {
        experienceFloat = PlayerPrefs.GetFloat("savedExperience");
        Debug.Log("EXPERIENCE:" + experienceFloat);
        for (int i = 0; i < 10; i++)
        {
            if (experienceFloat >= requiredExperience[i])
            {
                experienceLevel = i;
            }
        }

        if (gameStart == true)
        {
            depthMeter += Time.deltaTime * 10;          //depth metre starts
            DepthText.text = depthMeter.ToString("F0") + "m";
            CheckDepth();
            saveStatistics.playtimeSeconds = saveStatistics.playtimeSeconds + Time.deltaTime;
        }
        if(gameStart == false)
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

    private void CheckDepth()               //function which checks the players depth to see whether the level needs to be changed
    {
        if (depthMeter >= thisLevel.nextLevelTarget)
        {
            levelCounter++;
            thisLevel = levels[levelCounter];       //next level
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
        Time.timeScale = 1;
        pausedScreen.SetActive(false);
        gamePaused = false;
        gameStart = true;


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
        experienceText.text = "Experience: " + earntExperience.ToString("0");
    }

    private void ChangeLevel()              
    {
        //  PauseGame();
        //   depthScreen.DisplayScreen();
        // depthScreen.depth = depthMeter;
        if (ColorUtility.TryParseHtmlString("#" + thisLevel.cameraBackgroundColour, out Color colour))          //checks hex colour is accurate then changes the background to said colour
        {
            sceneColour = colour;

        }
        RenderSettings.fogDensity = thisLevel.depthDensity;                             //fog density is set to the value in the level
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
        saveInventory.Invoke("ReadSave", 0f);
        saveStatistics.Invoke("loadStats", 0f);
        achivementsManager.Invoke("ReadSave", 0f);

        saveInventory.ReadSave();
        saveStatistics.loadStats();
        achivementsManager.ReadSave();
        // dailyChallengesManager.ReadSave();
    }

    public void ResetMasterFunction()
    {
        //Debug.Log("SAVE DELETED - " + Time.time);
        //saveInventory.ResetSave();
        //saveStatistics.ResetStats();
        //achivementsManager.ResetSave();
        //dailyChallengesManager.ResetSave();
        // SaveMasterFunction();
    }

    public int GetChance(int objectType)                //function that returns the chance of spawning to the appropriote object
    {
        if (objectType == 1)
        {
            return thisLevel.smallFishChance;
        }
        else if(objectType == 2)
        {
            return thisLevel.mammelChance;
        }
        else
        {
            return thisLevel.abilitesChance;
        }

    }
}
