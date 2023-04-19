using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExperiencePageManager : MonoBehaviour
{

    #region Variables

    public GameObject deathScreen;                                  //The next UI Screen

    public float addedExperience;                                   //How much experience the player earnt
    public bool sentFromAchivements = false;                        //If the player has just ended a run or was sent from achivements as there is a different protocol

    [SerializeField] private GameObject barCurrent;                 //The current percentage of way through the level the player is
    [SerializeField] private GameObject barDisplacement;            //The new percentage of way through the level the player is

    [SerializeField] private TextMeshProUGUI levelCurrent;          //What current level is the player
    [SerializeField] private TextMeshProUGUI levelNext;             //What is the next level for the player to reach
    [SerializeField] private TextMeshProUGUI experienceEarnt;       //How much experience the player earnt in this event

    public bool claimExperience = false;                            //If the player pressed 'Claim Experience'
    public bool runOnce = false;                                    //Making sure it is only ran once

    [SerializeField] private float currentExperienceFloat;          //How much experience the player currently has
    [SerializeField] private float requiredExperienceFloat;         //The amount of required experience to level up
    [SerializeField] private int currentLevel;                      //The current level the player is on, for example Level 3.

    [SerializeField] private GameObject claimButtonObj;
    [SerializeField] private GameObject continueButtonObj;
    [SerializeField] private GameObject rewardsObj;


    [SerializeField] private float percentageCurrent;               //Calculated current percentage to the next level
    [SerializeField] private float percentageChange;                //Calculated percentage that the players experience changes.

    [SerializeField] private int lootcratesEarnt;
    [SerializeField] private TextMeshProUGUI lootcratesEarntText;
    [SerializeField] private GameObject claimButton;

    [SerializeField] private GameObject crateObject;                //The game object of the lootcrate function
    [SerializeField] private ScrollCrate scrollCrate;               //The script for the lootcrate function


    public float newExperience;

    #endregion


    private void Awake()
    {
        addedExperience = GameManager.currentManager.earntExperience;
        currentExperienceFloat = PlayerPrefs.GetFloat("savedExperience");
        currentLevel = GameManager.currentManager.experienceLevel; //Get the current level
        requiredExperienceFloat = GameManager.currentManager.requiredExperience[currentLevel + 1]; //Get the amount required to reach the next level

    }
    void Start()
    {
        GameManager.currentManager.ResumeGame();

        Debug.Log("Added:" + addedExperience);
        Debug.Log("Current:" + currentExperienceFloat);
        Debug.Log("Current Level:" + currentLevel);
        Debug.Log("Required Experience:" + requiredExperienceFloat);


        //  claimExperience = true;
        if (requiredExperienceFloat != 0f)
        {
            percentageCurrent = ((currentExperienceFloat - GameManager.currentManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
        }

        percentageCurrent = ((currentExperienceFloat - GameManager.currentManager.requiredExperience[currentLevel]) / requiredExperienceFloat); //Calculate the percentage bar scale
        experienceEarnt.text = addedExperience.ToString("0"); //Set the text to the correct experience
        lootcratesEarnt = 1;
        PlayerPrefs.SetInt("LootcratesHolding", PlayerPrefs.GetInt("LootcratesHolding") + 1);
        PlayerPrefs.Save();
        Debug.Log(percentageCurrent);

        if (percentageCurrent > 0)
        {
            barCurrent.transform.localScale = new Vector2(percentageCurrent * 2, 1);
        }

    }

    void Update()
    {
        if (GameManager.currentManager.experienceLevel == currentLevel + 1) //If the player levels up
        {
            barCurrent.gameObject.SetActive(false); //Remove the old current bar as it would now be wrong
        }


        currentLevel = GameManager.currentManager.experienceLevel; //Get the current level
        requiredExperienceFloat = GameManager.currentManager.requiredExperience[currentLevel + 1]; //Get the amount required to reach the next level

        //   currentExperienceFloat = PlayerPrefs.GetFloat("savedExperience"); //Get the current experience from PlayerPrefs

        levelCurrent.text = currentLevel.ToString();
        levelNext.text = (currentLevel + 1).ToString();

        if (runOnce == false)
        {
            runOnce = true;
            percentageCurrent = ((currentExperienceFloat - GameManager.currentManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
        }

        if (GameManager.currentManager.experienceFloat >= 1)
        {
            percentageChange = ((currentExperienceFloat - GameManager.currentManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
        }




        if (addedExperience > 0 && claimExperience == true && percentageChange <= 1)
        {
            addedExperience -= Time.deltaTime * 100;
            percentageCurrent = ((currentExperienceFloat - Time.deltaTime * 200 - GameManager.currentManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
            PlayerPrefs.SetFloat("savedExperience", PlayerPrefs.GetFloat("savedExperience") + Time.deltaTime * 100); //Add experience over time to animate the bar
            currentExperienceFloat = PlayerPrefs.GetFloat("savedExperience");
            barDisplacement.transform.localScale = new Vector2(percentageChange * 2, 1); //Scale the percentage bar
        }

        if (lootcratesEarnt >= 1 && PlayerPrefs.GetInt("LootcratesHolding") >= 1) //If any lootcrates have been earnt
        {
            claimButton.SetActive(true); //Enable the button to open them
        }
        else
        {
            claimButton.SetActive(false); //Otherwise, remove the button.
        }

    }
    /// <summary>
    /// Button ran when the player clicks on the "Claim Experience" button
    /// </summary>
    public void ClaimExperience()
    {

        claimExperience = true;
        claimButtonObj.SetActive(false);
        continueButtonObj.SetActive(true);
        GameManager.currentManager.SaveMasterFunction();
    }
    /// <summary>
    /// Button ran when the player clicks on the "Play Again" button
    /// </summary>
    public void Continue()
    {
        PlayerPrefs.SetFloat("savedExperience", currentExperienceFloat += addedExperience); //Save the experience to PlayerPrefs
        PlayerPrefs.Save();

        if (sentFromAchivements == false)
        {
            rewardsObj.SetActive(true);
        }
        else
        {
            deathScreen.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Button runs this function when the player claims a lootcrate from the achivements tab
    /// </summary>
    public void OpenCrateFromAchivements()
    {

        lootcratesEarnt--;
        GameManager.currentManager.PauseGame();

        crateObject.SetActive(true);
        scrollCrate.StartRoll();

    }
    /// <summary>
    /// Button runs this function when the player claims a lootcrate at the end of a run
    /// </summary>
    public void OpenCrate()
    {
        if (PlayerPrefs.GetInt("LootcratesHolding") >= 1)
        {
            PlayerPrefs.SetInt("LootcratesHolding", (PlayerPrefs.GetInt("LootcratesHolding") - 1));
            if (sentFromAchivements == false)
            {
                GameManager.currentManager.PauseGame();
            }
            crateObject.SetActive(true);
            scrollCrate.StartRoll();
            lootcratesEarnt--;
        }
    }

    /// <summary>
    /// Button runs this function when the lootcrate process is complete
    /// </summary>
    public void ReturnToGame()
    {
        GameManager.currentManager.SaveMasterFunction();

        if (sentFromAchivements == false)
        {
            GameManager.currentManager.ResumeGame();
            SceneManager.LoadScene("Scene");
        }
        else
        {
            this.gameObject.SetActive(false);
            deathScreen.SetActive(false);
        }
    }

    public void QuickReplay()
    {
        PlayerPrefs.SetInt("LootcratesHolding", PlayerPrefs.GetInt("LootcratesHolding") + 1);
        PlayerPrefs.Save();
        ReturnToGame();
    }
}