using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChallengesManager : MonoBehaviour
{
    private string[] challengeStringPart1 = new string[10];
    private string[] challengeStringPart2 = new string[10];
    private int[] rangeMin = new int[10];
    private int[] rangeMax = new int[10];

    [SerializeField] private TextMeshProUGUI[] challengeString = new TextMeshProUGUI[5];
    [SerializeField] private GameObject[] percentageBar = new GameObject[5];
    [SerializeField] private GameObject[] button = new GameObject[5];


    public void Start()
    {


        challengeStringPart1[0] = "Travel ";
        rangeMin[0] = 500;
        rangeMax[0] = 1500;
        challengeStringPart2[0] = " M in a single run!";

        challengeStringPart1[1] = "Travel ";
        rangeMin[1] = 500;
        rangeMax[1] = 1500;
        challengeStringPart2[1] = " M underwater!";

        challengeStringPart1[2] = "Play ";
        rangeMin[2] = 3;
        rangeMax[2] = 10;
        challengeStringPart2[2] = " Runs";

        challengeStringPart1[3] = "Narrowly avoid ";
        rangeMin[3] = 10;
        rangeMax[3] = 30;
        challengeStringPart2[3] = "Objects close to your mech!";

        challengeStringPart1[4] = "Open ";
        rangeMin[4] = 2;
        rangeMax[4] = 5;
        challengeStringPart2[4] = " Lootcrates!";

        challengeStringPart1[5] = "Play for ";
        rangeMin[5] = 3;
        rangeMax[5] = 6;
        challengeStringPart2[5] = " Minutes in a single run without dying!";

        challengeStringPart1[6] = "Enter ";
        rangeMin[6] = 2;
        rangeMax[6] = 4;
        challengeStringPart2[6] = "Zones in the ocean!";

        SetChallenge(0);
        SetChallenge(1);
        SetChallenge(2);
        SetChallenge(3);
        SetChallenge(4);
    }

    public void SetChallenge(int i)
    {
        int random = Random.Range(0, 5);      
        int range = Random.Range(rangeMin[random], rangeMax[random]);
        challengeString[i].text += challengeStringPart1[random].ToString();
        challengeString[i].text += range.ToString();
        challengeString[i].text += challengeStringPart2[random];

    }
}
