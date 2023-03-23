using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class AchivementsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] challengeObject = new GameObject[30];
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



    public void Awake()
    {
        //  path = Application.streamingAssetsPath + "/Saves" + "/Achivement" + ".txt";
    }

    public void UnlockAll()
    {
        for (int i = 0; i < 27; i++)
        {
            achivementScript[i].ClaimReward();
        }
    }

    public void Update()
    {
        experienceRewardText.text = experienceReward + " Experience";
        lootcratesRewardText.text = lootcratesReward + " Lootcrates";

        if (experienceReward > 0)
        {
            claimExperience.SetActive(true);
        }
        else
        {
            claimExperience.SetActive(false);
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

    public void ClaimExperience()
    {
        deathScreen.SetActive(true);
        experienceBar.SetActive(true);
        experiencePageManager.addedExperience = experienceReward;
        experiencePageManager.sentFromAchivements = true;
        experienceReward = 0;
    }

    public void ClaimLootcrate()
    {
        lootcratesReward--;
        Crate.SetActive(true);
        scrollCrate.StartRoll();
    }

    public void SaveAchievements()
    {
        for (int i = 0; i < 27; i++)
        {
            PlayerPrefs.SetString("AchivementStatus" + i, achivementScript[i].claimed.ToString());
        }
        Debug.Log("Achivements Saved");

    }

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

    public void ResetSave()
    {
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < 27; i++)
        {
            writer.WriteLine("False");
        }
        writer.Close();
    }
}
