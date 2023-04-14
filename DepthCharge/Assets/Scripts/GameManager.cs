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
        Invoke("LoadMasterFunction", 0.01f);
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
    [SerializeField] private GameObject tutorialObject;

    public int experienceLevel;
    public float experienceFloat;
    public float[] requiredExperience = new float[50];

    public float earntExperience;

    private bool gamePaused;

    #endregion


    public void Start()
    {
        Debug.Log(PlayerPrefs.GetFloat("savedRuns") + "SAVED RUNS");
        if(PlayerPrefs.GetFloat("savedRuns") == 0)
        {
            tutorialObject.SetActive(true);
        }
        else
        {
            tutorialObject.SetActive(false);
        }




        Time.timeScale = 0.01f; //Pause time
        sceneColour = cam.backgroundColor;      //scene colour is the cameras background
        saveStatistics.runs++; //Add to the amount of times launched.
        saveStatistics.saveStats();
        thisLevel = levels[0];



        //The experience required to reach that level...
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

        for (int i = 0; i < 10; i++) //Run 10 times
        {
            if (experienceFloat >= requiredExperience[i]) //If the player has enough experience 
            {
                experienceLevel = i; //Set that level to the currnet level, will always be the highest possible
            }
        }

        if (gameStart == true)
        {
            depthMeter += Time.deltaTime * 10;          //depth metre starts
            DepthText.text = depthMeter.ToString("F0") + "m"; //Set the Depth text on the cockpit
            CheckDepth();
            saveStatistics.playtimeSeconds = saveStatistics.playtimeSeconds + Time.deltaTime;
        }
        if (gameStart == false) //If the game hasn't started yet
        {
            if (Input.GetKeyDown(KeyCode.Space)) //And the player presses Space
            {
                StartGame(); //Start the Game process
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
    /// <summary>
    /// Button runs function which returns the player to the main menu
    /// </summary>
    public void ReturnToMenu()
    {
        SaveMasterFunction();
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Button runs function that resumes the progress of the game
    /// </summary>
    public void ResumeGame()
    {
        pausedScreen.SetActive(false);
        gamePaused = false;
        Time.timeScale = 1;
    }

    /// <summary>
    /// Button runs function which starts the process of the game.
    /// </summary>
    public void StartGame()
    {
        Time.timeScale = 1;
        pausedScreen.SetActive(false);
        gamePaused = false;
        gameStart = true;
    }

    /// <summary>
    /// Button runs function which slows down gameplay and enables a canvas for Paused game info
    /// </summary>
    public void PauseGame()
    {
        gamePaused = true;
        pausedScreen.SetActive(true);
        Time.timeScale = 0.01f;
    }

    /// <summary>
    /// Toggle between Paused and Unpaused.
    /// </summary>
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


    /// <summary>
    /// Function runs when the player dies, 
    /// Sets all the text as the stats of that run
    /// </summary>
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
    }

    public int GetChance(int objectType)                //function that returns the chance of spawning to the appropriote object
    {
        if (objectType == 1)
        {
            return thisLevel.smallFishChance;
        }
        else if (objectType == 2)
        {
            return thisLevel.mammelChance;
        }
        else
        {
            return thisLevel.abilitesChance;
        }

    }
}
