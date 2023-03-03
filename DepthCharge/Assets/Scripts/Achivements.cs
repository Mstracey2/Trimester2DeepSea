using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Achivements : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] Statistics statistics;
    [SerializeField] AchivementsManager achivementsManager;

    [SerializeField] private string[] achivementString = new string[30];
    [SerializeField] private int[] difficulty = new int[30];
    [SerializeField] private Sprite[] imageCover = new Sprite[30];
    [SerializeField] private int[] requiredInt = new int[30];
    [SerializeField] private string[] statisticAssosiated = new string[30];

    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image achivementImage;
    [SerializeField] private RawImage difficultyFilter;
    [SerializeField] private TextMeshProUGUI rewardText;
    [SerializeField] private TextMeshProUGUI percentageText;

    [SerializeField] private Image claimButton;
    [SerializeField] private Sprite claimedSprite;
    [SerializeField] private Image percentageBar;
    [SerializeField] private float percentageComplete;
    [SerializeField] private TextMeshProUGUI progressText;

    public int achivementNumber;
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
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        statistics = gameManager.GetComponent<Statistics>();
        achivementsManager = (AchivementsManager)FindObjectOfType(typeof(AchivementsManager));


        achivementString[0] = "Play 5 Runs";
        difficulty[0] = 1;
        requiredInt[0] = 5;
        statisticAssosiated[0] = "runs";

        achivementString[1] = "Play 15 Runs";
        difficulty[1] = 2;
        requiredInt[1] = 15;
        statisticAssosiated[1] = "runs";

        achivementString[2] = "Play 30 Runs";
        difficulty[2] = 3;
        requiredInt[2] = 30;
        statisticAssosiated[2] = "runs";

        achivementString[3] = "Play 50 Runs";
        difficulty[3] = 4;
        requiredInt[3] = 50;
        statisticAssosiated[3] = "runs";

        achivementString[4] = "Play 100 Runs";
        difficulty[4] = 5;
        requiredInt[4] = 100;
        statisticAssosiated[4] = "runs";


        achivementString[5] = "Buy 3 Items";
        difficulty[5] = 1;
        requiredInt[5] = 3;
        statisticAssosiated[5] = "itemsBought";

        achivementString[6] = "Buy 7 Items";
        difficulty[6] = 2;
        requiredInt[6] = 7;
        statisticAssosiated[6] = "itemsBought";

        achivementString[7] = "Buy 12 Items";
        difficulty[7] = 3;
        requiredInt[7] = 12;
        statisticAssosiated[7] = "itemsBought";

        achivementString[8] = "Buy 20 Items";
        difficulty[8] = 4;
        requiredInt[8] = 20;
        statisticAssosiated[8] = "itemsBought";

        achivementString[9] = "Buy 30 Items";
        difficulty[9] = 5;
        requiredInt[9] = 30;
        statisticAssosiated[9] = "itemsBought";


        achivementString[10] = "Reach The Twilight Zone";
        difficulty[10] = 1;
        //  requiredInt[10] = ;

        achivementString[11] = "Reach The Midnight Zone";
        difficulty[11] = 2;

        achivementString[12] = "Reach The Abyss Zone";
        difficulty[12] = 3;

        achivementString[13] = "Reach The Trenches Zone";
        difficulty[13] = 4;

        achivementString[14] = "Reach The Unknowns Zone";
        difficulty[14] = 5;


        achivementString[15] = "Play for 5 Minutes";
        difficulty[15] = 1;
        requiredInt[15] = 300;
        statisticAssosiated[15] = "playtimeSeconds";

        achivementString[16] = "Play for 25 Minutes";
        difficulty[16] = 2;
        requiredInt[16] = 1500;
        statisticAssosiated[16] = "playtimeSeconds";

        achivementString[17] = "Play for 60 Minutes";
        difficulty[17] = 3;
        requiredInt[17] = 3600;
        statisticAssosiated[17] = "playtimeSeconds";

        achivementString[18] = "Play for 120 Minutes";
        difficulty[18] = 4;
        requiredInt[18] = 7200;
        statisticAssosiated[18] = "playtimeSeconds";

        achivementString[19] = "Play for 300 Minutes";
        difficulty[19] = 5;
        requiredInt[19] = 18000;
        statisticAssosiated[19] = "playtimeSeconds";



        achivementString[20] = "Launch the game";
        difficulty[20] = 1;
        requiredInt[20] = 1;
        statisticAssosiated[20] = "timesLaunched";


        achivementString[21] = "Launch the game 3 times";
        difficulty[21] = 2;
        requiredInt[21] = 3;
        statisticAssosiated[21] = "timesLaunched";

        achivementString[22] = "Launch the game 5 times";
        difficulty[22] = 3;
        requiredInt[22] = 5;
        statisticAssosiated[22] = "timesLaunched";

        achivementString[23] = "Launch the game 10 times";
        difficulty[23] = 4;
        requiredInt[23] = 10;
        statisticAssosiated[23] = "timesLaunched";

        achivementString[24] = "Launch the game 15 times";
        difficulty[24] = 5;
        requiredInt[24] = 15;
        statisticAssosiated[24] = "timesLaunched";


        achivementString[25] = "Beat your Highscore";
        difficulty[25] = 3;
        requiredInt[25] = 1;

        achivementString[26] = "Review the game";
        difficulty[26] = 5;
        requiredInt[26] = 1;



        SetInformation();
        UpdateInformation();
    }

    public void SetInformation()
    {
        descriptionText.text = achivementString[achivementNumber];
        achivementImage.sprite = imageCover[achivementNumber];
        rewardText.text = (100 * difficulty[achivementNumber]).ToString();
        switch (difficulty[achivementNumber])
        {
            case 1:
                difficultyFilter.color = new Color32(19, 255, 0, 75);
                break;
            case 2:
                difficultyFilter.color = new Color32(236, 255, 0, 75);
                break;
            case 3:
                difficultyFilter.color = new Color32(255, 50, 0, 75);
                break;
            case 4:
                difficultyFilter.color = new Color32(255, 0, 175, 75);
                break;
            case 5:
                difficultyFilter.color = new Color32(0, 30, 255, 75);
                break;
        }
    }

    public void UpdateInformation()
    {
        progressText.text = "0 / 1";
        switch (statisticAssosiated[achivementNumber])
        {

            case "runs":
                percentageCurrent = statistics.runs / requiredInt[achivementNumber];

                progressText.text = statistics.runs + "/" + requiredInt[achivementNumber];
                break;

            case "itemsBought":
                percentageCurrent = statistics.itemsBought / requiredInt[achivementNumber];
                progressText.text = statistics.itemsBought + "/" + requiredInt[achivementNumber];
                break;

            case "playtimeSeconds":
                percentageCurrent = statistics.playtimeSeconds / requiredInt[achivementNumber];
                progressText.text = (statistics.playtimeSeconds / 60).ToString("0") + "/" + (requiredInt[achivementNumber] / 60).ToString("0");
                break;

            case "timesLaunched":
                percentageCurrent = statistics.timesLaunched / requiredInt[achivementNumber];
                progressText.text = statistics.timesLaunched + "/" + requiredInt[achivementNumber];

                break;
        }
        if (percentageCurrent >= 1)
        {
            progressText.text = requiredInt[achivementNumber] + "/" + requiredInt[achivementNumber];
        }

        if (percentageCurrent <= 1 && percentageCurrent >= 0)
        {
            percentageBar.gameObject.transform.localScale = new Vector3(percentageCurrent, 1, 1);
            percentageText.text = (percentageCurrent * 100).ToString("0") + "%";
        }
        if (percentageCurrent >= 1)
        {
            percentageBar.gameObject.transform.localScale = new Vector3(1, 1, 1);
            percentageText.text = "100%";
        }



        if (claimed == false && percentageCurrent >= 1)
        {
            claimButton.sprite = unlockedButton;
        }


        if (claimed == true)
        {
            claimButton.sprite = claimedSprite;
            claimButton.GetComponent<Button>().interactable = false;
        }
    }

    public void ClaimReward()
    {
        if (percentageCurrent >= 1 && claimed == false)
        {
            claimed = true;
            UpdateInformation();
            achivementsManager.lootcratesReward++;
            achivementsManager.experienceReward += (difficulty[achivementNumber] * 100);
        }
    }
}
