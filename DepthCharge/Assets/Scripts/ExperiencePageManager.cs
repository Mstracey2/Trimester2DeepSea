using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ExperiencePageManager : MonoBehaviour
{

    public GameManager gameManager;

    public float addedExperience;

    [SerializeField] private GameObject barCurrent;
    [SerializeField] private GameObject barDisplacement;
    [SerializeField] private TextMeshProUGUI levelCurrent;
    [SerializeField] private TextMeshProUGUI levelNext;
    [SerializeField] private TextMeshProUGUI experienceEarnt;

    public bool claimExperience = false;

    [SerializeField] private float currentExperienceFloat;
    [SerializeField] private float requiredExperienceFloat;
    [SerializeField] private int currentLevel;



    [SerializeField] private float percentageCurrent;
    [SerializeField] private float percentageChange;


    [SerializeField] private int lootcratesEarnt;
    [SerializeField] private TextMeshProUGUI lootcratesEarntText;
    [SerializeField] private GameObject claimButton;

    [SerializeField] private GameObject crateObject;

    void Start()
    {      
        addedExperience = gameManager.earntExperience;
        percentageCurrent = ((currentExperienceFloat - gameManager.requiredExperience[currentLevel]) / requiredExperienceFloat);      
        experienceEarnt.text = addedExperience.ToString("0");
        lootcratesEarnt = 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentLevel = gameManager.experienceLevel;
        requiredExperienceFloat = gameManager.requiredExperience[currentLevel+1];
        currentExperienceFloat = gameManager.experienceFloat;
        levelCurrent.text = currentLevel.ToString();
        levelNext.text = (currentLevel + 1).ToString();

        if (gameManager.experienceFloat >= 1)
        {
            percentageChange  = ((currentExperienceFloat - gameManager.requiredExperience[currentLevel]) / requiredExperienceFloat);
        }


        if (addedExperience > 0 && claimExperience == true)
        {
            addedExperience -= Time.deltaTime*100;
            gameManager.experienceFloat += Time.deltaTime*100;

        barCurrent.transform.localScale = new Vector2(percentageCurrent,1);
        barDisplacement.transform.localScale = new Vector2(percentageChange, 1);
        }



        if(lootcratesEarnt >= 0)
        {
            claimButton.SetActive(true);
        }
        else
        {
            claimButton.SetActive(false);
        }

    }

    public void StartSequence()
    {


    }

    public void AddExperience()
    {


    }

    public void ClaimExperience()
    {
        claimExperience = true;
    }

    public void OpenCrate()
    {
        crateObject.SetActive(true);
        lootcratesEarnt--;
    }

    public void ReturnToGame()
    {
        gameManager.SaveMasterFunction();
        SceneManager.LoadScene("Scene");
    }
}
