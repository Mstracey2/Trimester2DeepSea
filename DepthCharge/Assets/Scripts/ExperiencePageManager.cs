using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExperiencePageManager : MonoBehaviour
{

    public GameManager gameManager;
    public GameObject deathScreen;

    public float addedExperience;

    public bool sentFromAchivements = false;

    [SerializeField] private GameObject barCurrent;
    [SerializeField] private GameObject barDisplacement;
    [SerializeField] private TextMeshProUGUI levelCurrent;
    [SerializeField] private TextMeshProUGUI levelNext;
    [SerializeField] private TextMeshProUGUI experienceEarnt;

    public bool claimExperience = false;
    public bool runOnce = false;

    [SerializeField] private float currentExperienceFloat;
    [SerializeField] private float requiredExperienceFloat;
    [SerializeField] private int currentLevel;

    [SerializeField] private GameObject claimButtonObj;
    [SerializeField] private GameObject continueButtonObj;
    [SerializeField] private GameObject rewardsObj;


    [SerializeField] private float percentageCurrent;
    [SerializeField] private float percentageChange;


    [SerializeField] private int lootcratesEarnt;
    [SerializeField] private TextMeshProUGUI lootcratesEarntText;
    [SerializeField] private GameObject claimButton;

    [SerializeField] private GameObject crateObject;
    [SerializeField] private ScrollCrate scrollCrate;

    void Start()
    {

        if (sentFromAchivements == false)
        {
            gameManager.ResumeGame();
            addedExperience = gameManager.earntExperience;
        }
        else
        {
            gameManager.ResumeGame();
        }


        percentageCurrent = ((currentExperienceFloat - gameManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
        experienceEarnt.text = addedExperience.ToString("0");
        lootcratesEarnt = 1;


    }

    void Update()
    {
        if(gameManager.experienceLevel == currentLevel + 1)
        {
            barCurrent.gameObject.SetActive(false);
        }
        currentLevel = gameManager.experienceLevel;
        requiredExperienceFloat = gameManager.requiredExperience[currentLevel + 1];
        currentExperienceFloat = gameManager.experienceFloat;
        levelCurrent.text = currentLevel.ToString();
        levelNext.text = (currentLevel + 1).ToString();

        if (runOnce == false) 
        {
            runOnce = true;
            percentageCurrent = ((currentExperienceFloat - gameManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
        }

        if (gameManager.experienceFloat >= 1)
        {
            percentageChange = ((currentExperienceFloat - gameManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
        }


        if (percentageCurrent > 0 )
        {
            barCurrent.transform.localScale = new Vector2(percentageCurrent * 2, 1);
        }


        if (addedExperience > 0 && claimExperience == true && percentageChange <= 1)
        {
            addedExperience -= Time.deltaTime * 100;
         //   percentageCurrent = ((currentExperienceFloat - Time.deltaTime * 200 - gameManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
            gameManager.experienceFloat += Time.deltaTime * 100;

            barDisplacement.transform.localScale = new Vector2(percentageChange * 2, 1);
        }



        if (lootcratesEarnt >= 1)
        {
            claimButton.SetActive(true);
        }
        else
        {
            claimButton.SetActive(false);
        }

    }

    public void ClaimExperience()
    {

        claimExperience = true;
        claimButtonObj.SetActive(false);
        continueButtonObj.SetActive(true);
        gameManager.SaveMasterFunction();
    }

    public void Continue()
    {
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


    public void OpenCrateFromAchivements()
    {    
        
        lootcratesEarnt--;
            gameManager.PauseGame();
        
        crateObject.SetActive(true);
        scrollCrate.StartRoll();

    }
    public void OpenCrate()
    {
        if (sentFromAchivements == false)
        {
            gameManager.PauseGame();
        }
        crateObject.SetActive(true);
        scrollCrate.StartRoll();
        lootcratesEarnt--;
    }

    public void ReturnToGame()
    {
        gameManager.SaveMasterFunction();

        if (sentFromAchivements == false)
        {
            gameManager.ResumeGame();
            SceneManager.LoadScene("Scene");
        }
        else
        {

            this.gameObject.SetActive(false);
            deathScreen.SetActive(false);
        }
    }
}
