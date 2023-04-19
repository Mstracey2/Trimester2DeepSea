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


    public int experienceReward;
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
    [SerializeField] private TextMeshProUGUI playersCoins;

    public bool playerMaxedInventory;

    #endregion
    /// <summary>
    /// Button on the UI which unlocks all completed achivements
    /// </summary>
    /// 
    public void Start()
    {
        ReadSave();
    }
    public void UnlockAll()
    {
        for (int i = 0; i < 27; i++) //Runs through all achivements
        {
            achivementScript[i].ClaimReward(); //And runs a function which unlocks it if its at 100% complete
        }
    }

    public void Update()
    {
        experienceRewardText.text = experienceReward + " Coins"; //Display the amount of experience the player currently has earnt
        lootcratesRewardText.text = lootcratesReward + " Lootcrates"; //Display the amount of loot crates the player has currently earnt
        playersCoins.text = PlayerPrefs.GetInt("PlayerCoins",0).ToString() + " Coins";

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
            //for (int inventoryNumber = 0; inventoryNumber < 19; inventoryNumber++) //Run for each inventory item
            //{
            //    if (PlayerPrefs.GetString("Inventory" + inventoryNumber).StartsWith("T") && playerMaxedInventory == true)
            //    {
            //        playerMaxedInventory = true;

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
        PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + experienceReward);
        experienceReward = 0;
    }

    /// <summary>
    /// Button is linked to the UI and will allow the player to open all earnt lootcrates.
    /// </summary>
    public void ClaimLootcrate()
    {
        PlayerPrefs.SetInt("LootcratesHolding", PlayerPrefs.GetInt("LootcratesHolding") - 1);
        PlayerPrefs.Save();
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
        PlayerPrefs.Save();
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
