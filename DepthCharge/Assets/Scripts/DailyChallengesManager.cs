using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class DailyChallengesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] challengeObject = new GameObject[30];
    [SerializeField] private Achivements[] achivementScript = new Achivements[30];

    public string path = "Assets/Saves/Daily Challenges.txt";
    public List<string> listOfStats = new List<string>(10);

    public float experienceReward;

    public GameObject deathScreen;
    public GameObject experienceBar;
    public ExperiencePageManager experiencePageManager;

    [SerializeField] public float day;
    [SerializeField] public float hour;
    [SerializeField] public float minute;
    [SerializeField] public float second;

    [SerializeField] private float hourUntil;
    [SerializeField] private float minuteUntil;
    [SerializeField] private float secondUnitl;

    [SerializeField] private TextMeshProUGUI hourUntilText;
    [SerializeField] private TextMeshProUGUI minuteUntilText;
    [SerializeField] private TextMeshProUGUI secondUntilText;

    [SerializeField] private int metersSingle;
    [SerializeField] private int metersTotal;
    [SerializeField] private int runs;
    [SerializeField] private int lootcratesOpened;
    [SerializeField] private int itemsBought;
    [SerializeField] private int itemsPutOn;

    [SerializeField] private GameObject claimExperience;

    [SerializeField] private TextMeshProUGUI experienceRewardText;


    public void Update()
    {
        second = float.Parse(System.DateTime.Now.ToString("ss"));
        minute = float.Parse(System.DateTime.Now.ToString("mm"));
        hour = float.Parse(System.DateTime.Now.ToString("HH"));

        day = float.Parse(System.DateTime.Now.ToString("dd"));

        secondUnitl = 60 - second;
        minuteUntil = 60 - minute;
        hourUntil = 24 - hour;

        hourUntilText.text = hourUntil.ToString("00") + " Hours";
        minuteUntilText.text = minuteUntil.ToString("00" + " Minutes");
        secondUntilText.text = secondUnitl.ToString("00" + " Seconds");

        listOfStats[0] = hour.ToString();
        listOfStats[1] = metersSingle.ToString();
        listOfStats[2] = metersTotal.ToString();
        listOfStats[3] = runs.ToString();
        listOfStats[4] = lootcratesOpened.ToString();
        listOfStats[5] = itemsBought.ToString();
        listOfStats[6] = itemsPutOn.ToString();
        //    experienceRewardText.text = experienceReward + " Experience";

        //if (experienceReward > 0)
        //{
        //    claimExperience.SetActive(true);
        //}
        //else
        //{
        //    //claimExperience.SetActive(false);
        //}
    }

    public void ClaimExperience()
    {
        deathScreen.SetActive(true);
        experienceBar.SetActive(true);
        experiencePageManager.addedExperience = experienceReward;
        experiencePageManager.sentFromAchivements = true;
        experienceReward = 0;
    }


    public void SaveChallenges()
    {
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < 10; i++)
        {
            writer.WriteLine(listOfStats[i]);
        }

        writer.Close();
    }

    public void ReadSave()
    {
        string[] lines = System.IO.File.ReadAllLines(path);

        using StreamReader reader = new StreamReader(path);
        day = int.Parse(lines[1]);
        metersSingle = int.Parse(lines[2]);
        metersTotal = int.Parse(lines[3]);
        runs = int.Parse(lines[4]);
        lootcratesOpened = int.Parse(lines[5]);
        itemsBought = int.Parse(lines[6]);
        itemsPutOn = int.Parse(lines[7]);

        if (day != int.Parse(System.DateTime.Now.ToString("dd")))
        {
            Debug.Log("NOT SAME DAY");
            ResetSave();
        }
        else
        {
            Debug.Log("SAME DAY");
        }

    }

    public void ResetSave()
    {
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path, true);

        writer.WriteLine(System.DateTime.Now.ToString("dd"));

        for (int i = 0; i < 10; i++)
        {
            writer.WriteLine("0");
        }
        writer.Close();
    }
}
