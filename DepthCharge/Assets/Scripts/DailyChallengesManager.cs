using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;


public class DailyChallengesManager : MonoBehaviour
{
    [SerializeField] private GameObject[] challengeObject = new GameObject[10];
    [SerializeField] private DailyChallenges[] DailyChallenges = new DailyChallenges[5];

    public string path = "Assets/Saves/Daily Challenges.txt";

    public List<string> listOfStats = new List<string>(10);
    public List<string> listOfChallenges = new List<string>(15);

    public float experienceReward;

    public GameObject deathScreen;
    public GameObject experienceBar;
    public ExperiencePageManager experiencePageManager;

    [SerializeField] public int day;
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

    public void Start()
    {
        listOfStats[0] = 0.ToString();
        listOfStats[1] = day.ToString();
        listOfStats[2] = metersSingle.ToString();
        listOfStats[3] = metersTotal.ToString();
        listOfStats[4] = runs.ToString();
        listOfStats[5] = lootcratesOpened.ToString();
        listOfStats[6] = itemsBought.ToString();
        listOfStats[7] = itemsPutOn.ToString();
    }

    public void Update()
    {
        second = float.Parse(System.DateTime.Now.ToString("ss"));
        minute = float.Parse(System.DateTime.Now.ToString("mm"));
        hour = int.Parse(System.DateTime.Now.ToString("HH"));

        day = int.Parse(System.DateTime.Now.ToString("dd"));

        secondUnitl = 60 - second;
        minuteUntil = 60 - minute;
        hourUntil = 24 - hour;

        hourUntilText.text = hourUntil.ToString("00") + " Hours";
        minuteUntilText.text = minuteUntil.ToString("00" + " Minutes");
        secondUntilText.text = secondUnitl.ToString("00" + " Seconds");

        
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

    public void SetNewChallenges()
    {

        listOfStats[0] = 0.ToString();
        listOfStats[1] = day.ToString();
        listOfStats[2] = 0.ToString();
        listOfStats[3] = 0.ToString();
        listOfStats[4] = 0.ToString();
        listOfStats[5] = 0.ToString();
        listOfStats[6] = 0.ToString();
        listOfStats[7] = 0.ToString();
        for (int i = 0; i < 5; i++)
        {
            int random = Random.Range(0, 5);
            listOfChallenges[i] = i.ToString(); ;
            listOfChallenges[i+5] = Random.Range(DailyChallenges[i].rangeMin[i], DailyChallenges[i].rangeMax[i]).ToString();
           // DailyChallenges[i].challengeNumber = random;
        }

        Invoke("SaveChallenges",0.1f);
    }

    public void LoadCurrentChallenges()
    {
        for(int i = 0; i < 5; i++)
        {
            DailyChallenges[i].challengeNumber =  int.Parse(listOfChallenges[i]);
            DailyChallenges[i].required = int.Parse(listOfChallenges[5 + i]);
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


    public void SaveChallenges()
    {
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < 8; i++)
        {
            writer.WriteLine(listOfStats[i]);
        }
        for (int j = 1; j < 11; j++)
        {
            writer.WriteLine(listOfChallenges[j]);
        }
        Debug.Log("Saved");

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

        listOfChallenges[1] = lines[8];
        listOfChallenges[2] = lines[9];
        listOfChallenges[3] = lines[10];
        listOfChallenges[4] = lines[11];
        listOfChallenges[5] = lines[12];
        listOfChallenges[6] = lines[13];
        listOfChallenges[7] = lines[14];
        listOfChallenges[8] = lines[15];
        listOfChallenges[9] = lines[16];
        listOfChallenges[10] =lines[17];




        Debug.Log("READ");

        if (day != int.Parse(System.DateTime.Now.ToString("dd")))
        {
            Debug.Log("NOT SAME DAY");

            Invoke("ResetSave",0.01f);
            SetNewChallenges();
        }
        if (day == int.Parse(System.DateTime.Now.ToString("dd")))
        {
            Debug.Log("SAME DAY");
            LoadCurrentChallenges();
        }

    }

    public void ResetSave()
    {

        
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("");
        writer.WriteLine(System.DateTime.Now.ToString("dd"));

        for (int i = 0; i < 20; i++)
        {
            writer.WriteLine("0");
        }

        writer.Close();

        SaveChallenges();
    }
}
