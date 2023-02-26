using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Achivements : MonoBehaviour
{
    [SerializeField] private string[] achivementString = new string[30];
    [SerializeField] private int[] difficulty = new int[30];
    [SerializeField] private Sprite[] imageCover = new Sprite[30];

    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image achivementImage;
    [SerializeField] private RawImage difficultyFilter;
    [SerializeField] private TextMeshProUGUI rewardText;

    [SerializeField] private Image percentageBar;
    [SerializeField] private float percentageComplete;

    public int achivementNumber;

    //Easy = 1
    //Medium = 2
    //Hard = 3
    //Extreme = 4
    //Legendary = 5

    // Start is called before the first frame update
    void Start()
    {

        achivementString[0] = "Play 5 Runs";
        difficulty[0] = 1;

        achivementString[1] = "Play 15 Runs";
        difficulty[1] = 2;

        achivementString[2] = "Play 30 Runs";
        difficulty[2] = 3;

        achivementString[3] = "Play 50 Runs";
        difficulty[3] = 4;

        achivementString[4] = "Play 100 Runs";
        difficulty[4] = 5;



        achivementString[5] = "Buy 3 Items";
        difficulty[5] = 1;

        achivementString[6] = "Buy 7 Items";
        difficulty[6] = 2;

        achivementString[7] = "Buy 12 Items";
        difficulty[7] = 3;

        achivementString[8] = "Buy 20 Items";
        difficulty[8] = 4;

        achivementString[9] = "Buy 30 Items";
        difficulty[9] = 5;


        achivementString[10] = "Reach The Twilight Zone";
        difficulty[10] = 1;

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

        achivementString[16] = "Play for 25 Minutes";
        difficulty[16] = 2;

        achivementString[17] = "Play for 60 Minutes";
        difficulty[17] = 3;

        achivementString[18] = "Play for 120 Minutes";
        difficulty[18] = 4;

        achivementString[19] = "Play for 300 Minutes";
        difficulty[19] = 5;


        achivementString[20] = "Launch the game";
        difficulty[20] = 1;

        achivementString[21] = "Launch the game 3 times";
        difficulty[21] = 2;

        achivementString[22] = "Launch the game 5 times";
        difficulty[22] = 3;

        achivementString[23] = "Launch the game 10 times";
        difficulty[23] = 4;

        achivementString[24] = "Launch the game 15 times";
        difficulty[24] = 5;

        achivementString[25] = "Beat your Highscore";
        difficulty[25] = 3;

        achivementString[26] = "Review the game";
        difficulty[26] = 5;



        SetInformation();
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
}
