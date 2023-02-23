using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExperiencePageManager : MonoBehaviour
{

    public GameManager gameManager;

    public float addedExperience;

    [SerializeField] private GameObject barCurrent;
    [SerializeField] private GameObject barDisplacement;
    [SerializeField] private TextMeshProUGUI levelCurrent;
    [SerializeField] private TextMeshProUGUI levelNext;
    [SerializeField] private TextMeshProUGUI experienceEarnt;

    [SerializeField] private float currentExperienceFloat;
    [SerializeField] private float requiredExperienceFloat;
    [SerializeField] private int currentLevel;

    [SerializeField] private float percentageCurrent;
    [SerializeField] private float percentageChange;
    void Start()
    {
        
        percentageCurrent = ((currentExperienceFloat - gameManager.requiredExperience[currentLevel]) / requiredExperienceFloat);      
        experienceEarnt.text = addedExperience.ToString();
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


        if (addedExperience > 0)
        {
            addedExperience -= Time.deltaTime*100;
            gameManager.experienceFloat += Time.deltaTime*100;
        }
        barCurrent.transform.localScale = new Vector2(percentageCurrent,1);
        barDisplacement.transform.localScale = new Vector2(percentageChange, 1);
    }

    public void StartSequence()
    {


    }

    public void AddExperience()
    {


    }
}
