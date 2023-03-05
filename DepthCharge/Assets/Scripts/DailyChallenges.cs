using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DailyChallenges : MonoBehaviour
{
    [SerializeField] Statistics statistics;
    [SerializeField] DailyChallengesManager dailyChallengesManager;

    [SerializeField] private string[] challengeString = new string[30];
    [SerializeField] private string[] challengeString2 = new string[30];
    [SerializeField] private Sprite[] imageCover = new Sprite[30];
    [SerializeField] public int[] rangeMin = new int[10];
    [SerializeField] public int[] rangeMax = new int[10];
    [SerializeField] private string[] statisticAssosiated = new string[30];

    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image achivementImage;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI percentageText;
    [SerializeField] public int required;

    [SerializeField] private Image claimButton;
    [SerializeField] private Sprite claimedSprite;
    [SerializeField] private Image percentageBar;
    [SerializeField] private float percentageComplete;
    [SerializeField] private TextMeshProUGUI progressText;

    public int challengeNumber;
    public bool claimed;
    public Sprite unlockedButton;
    public float percentageCurrent;

    //Easy = 1
    //Medium = 2
    //Hard = 3
    //Extreme = 4
    //Legendary = 5

    // Start is called before the first frame update
    void Start()
    {
        statistics = GameManager.currentManager.GetComponent<Statistics>();
        dailyChallengesManager = (DailyChallengesManager)FindObjectOfType(typeof(DailyChallengesManager));


        challengeString[0] = "Travel ";
        challengeString2[0] = "M in a single run!";
        rangeMin[0] = 500;
        rangeMax[0] = 1500;
        statisticAssosiated[0] = "metersSingle";

        challengeString[1] = "Travel ";
        challengeString2[1] = "M overall!";
        rangeMin[1] = 1000;
        rangeMax[1] = 3000;
        statisticAssosiated[1] = "metersTotal";

        challengeString[2] = "Play ";
        challengeString2[2] = " Games Today";
        rangeMin[2] = 3;
        rangeMax[2] = 10;
        statisticAssosiated[2] = "runs";

        challengeString[3] = "Open ";
        challengeString2[3] = " Lootcrates Today!";
        rangeMin[3] = 2;
        rangeMax[3] = 8;
        statisticAssosiated[3] = "lootcratesOpened";

        challengeString[4] = "Buy ";
        challengeString2[4] = " Items Today";
        rangeMin[4] = 1;
        rangeMax[4] = 5;
        statisticAssosiated[4] = "itemsBought";

        challengeString[5] = "Change Your Cosmetics!";
        challengeString2[5] = "";
        rangeMin[5] = 1;
        rangeMax[5] = 1;
        statisticAssosiated[5] = "itemsPutOn";

        // SetInformation();
        // UpdateInformation();

      //  required = Random.Range(rangeMin[challengeNumber], rangeMax[challengeNumber]);
    }

    public void Update()
    {
        descriptionText.text = challengeString[challengeNumber] + required + challengeString2[challengeNumber];
        achivementImage.sprite = imageCover[challengeNumber];
        rewardText.text = (100).ToString();
    }

    public void SetInformation(int challengeNumber, int required)
    {

    }
}

    //public void UpdateInformation()
    //{
    //    progressText.text = "0 / 1";
    //    switch (statisticAssosiated[challengeNumber])
    //    {

    //        case "runs":
    //            percentageCurrent = statistics.runs / requiredInt[challengeNumber];

    //            progressText.text = statistics.runs + "/" + requiredInt[challengeNumber];
    //            break;

    //        case "itemsBought":
    //            percentageCurrent = statistics.itemsBought / requiredInt[challengeNumber];
    //            progressText.text = statistics.itemsBought + "/" + requiredInt[challengeNumber];
    //            break;

    //        case "playtimeSeconds":
    //            percentageCurrent = statistics.playtimeSeconds / requiredInt[challengeNumber];
    //            progressText.text = (statistics.playtimeSeconds / 60).ToString("0") + "/" + (requiredInt[challengeNumber] / 60).ToString("0");
    //            break;

    //        case "timesLaunched":
    //            percentageCurrent = statistics.timesLaunched / requiredInt[challengeNumber];
    //            progressText.text = statistics.timesLaunched + "/" + requiredInt[challengeNumber];

    //            break;
    //    }
    //    if (percentageCurrent >= 1)
    //    {
    //        progressText.text = requiredInt[challengeNumber] + "/" + requiredInt[challengeNumber];
    //    }

    //    if (percentageCurrent <= 1 && percentageCurrent >= 0)
    //    {
    //        percentageBar.gameObject.transform.localScale = new Vector3(percentageCurrent, 1, 1);
    //        percentageText.text = (percentageCurrent * 100).ToString("0") + "%";
    //    }
    //    if (percentageCurrent >= 1)
    //    {
    //        percentageBar.gameObject.transform.localScale = new Vector3(1, 1, 1);
    //        percentageText.text = "100%";
    //    }



//        if (claimed == false && percentageCurrent >= 1)
//        {
//            claimButton.sprite = unlockedButton;
//        }


//        if (claimed == true)
//        {
//            claimButton.sprite = claimedSprite;
//            claimButton.GetComponent<Button>().interactable = false;
//        }
//    }

//    public void ClaimReward()
//    {
//        if (percentageCurrent >= 1 && claimed == false)
//        {
//            claimed = true;
//            UpdateInformation();
//            dailyChallengesManager.experienceReward += (difficulty[challengeNumber] * 100);
//        }
//    }
//}