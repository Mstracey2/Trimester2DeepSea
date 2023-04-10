using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class AchivementsManager : MonoBehaviour
{

    #region Variables
    //   [SerializeField] private GameObject[] challengeObject = new GameObject[30]; 
    [SerializeField] private Achivements[] achivementScript = new Achivements[30];

    public string path;


    public float experienceReward;
    public float lootcratesReward;

    public GameObject Crate;
    public ScrollCrate scrollCrate;

    public GameObject deathScreen;
    public GameObject experienceBar;
    public ExperiencePageManager experiencePageManager;

    [SerializeField] private GameObject claimLootCrate;
    [SerializeField] private GameObject claimExperience;

    [SerializeField] private TextMeshProUGUI experienceRewardText;
    [SerializeField] private TextMeshProUGUI lootcratesRewardText;

    #endregion
    /// <summary>
    /// Button on the UI which unlocks all completed achivements
    /// </summary>
    public void UnlockAll()
    {
        for (int i = 0; i < 27; i++) //Runs through all achivements
        {
            achivementScript[i].ClaimReward(); //And runs a function which unlocks it if its at 100% complete
        }
    }

    public void Update()
    {
        experienceRewardText.text = experienceReward + " Experience"; //Display the amount of experience the player currently has earnt
        lootcratesRewardText.text = lootcratesReward + " Lootcrates"; //Display the amount of loot crates the player has currently earnt

        if (experienceReward > 0) //If there is any experience waiting to be claimed...
        {
            claimExperience.SetActive(true); //Enable the button to collect it
        }
        else
        {
            claimExperience.SetActive(false); //Else, hide the button
        }

        if (lootcratesReward > 0)
        {
            claimLootCrate.SetActive(true);
        }
        else
        {
            claimLootCrate.SetActive(false);
        }
    }


    /// <summary>
    /// Button is linked to the UI and will add all earnt experience to the players total. 
    /// </summary>
    public void ClaimExperience()
    {
        deathScreen.SetActive(true);
        experienceBar.SetActive(true);
        experiencePageManager.addedExperience = experienceReward;
        experiencePageManager.sentFromAchivements = true;
        experienceReward = 0;
    }

    /// <summary>
    /// Button is linked to the UI and will allow the player to open all earnt lootcrates.
    /// </summary>
    public void ClaimLootcrate()
    {
        lootcratesReward--;
        Crate.SetActive(true);
        scrollCrate.StartRoll();
    }
    /// <summary>
    /// Saves the claimed status of each achivement.
    /// It is not required to save the individual statistics here because it recalulates it every time from the players statistics. 
    /// </summary>
    public void SaveAchievements()
    {
        for (int i = 0; i < 27; i++) //Run 27 times.
        {
            PlayerPrefs.SetString("AchivementStatus" + i, achivementScript[i].claimed.ToString()); //Save if the player has claimed the achivement yet to ensure each is only claimed once.
        }
    }
    /// <summary>
    /// Read the PlayerPrefs save and if it has been previously claimed, set it to be claimed again.
    /// </summary>
    public void ReadSave()
    {
        for (int i = 0; i < 27; i++)
        {
            if (PlayerPrefs.GetString("AchivementStatus" + i).StartsWith("T"))
            {
                achivementScript[i].claimed = true;
                achivementScript[i].UpdateInformation();
            }
        }
    }
}
