using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Achivements : MonoBehaviour
{
    [SerializeField] private string[] achivementString = new string[30];

    // Start is called before the first frame update
    void Start()
    {
        achivementString[0] = "Play 5 Runs";
        achivementString[1] = "Play 15 Runs";
        achivementString[2] = "Play 30 Runs";
        achivementString[3] = "Play 50 Runs";
        achivementString[4] = "Play 100 Runs";

        achivementString[5] = "Buy 3 Items";
        achivementString[6] = "Buy 7 Items";
        achivementString[7] = "Buy 12 Items";
        achivementString[8] = "Buy 20 Items";
        achivementString[9] = "Buy 30 Items";

        achivementString[10] = "Reach The Twilight Zone";
        achivementString[11] = "Reach The Midnight Zone";
        achivementString[12] = "Reach The Abyss Zone";
        achivementString[13] = "Reach The Trenches Zone";
        achivementString[14] = "Reach The Unknowns Zone";

        achivementString[15] = "Play for 5 Minutes";
        achivementString[16] = "Play for 25 Minutes";
        achivementString[17] = "Play for 60 Minutes";
        achivementString[18] = "Play for 120 Minutes";
        achivementString[19] = "Play for 300 Minutes";

        achivementString[20] = "Launch the game";
        achivementString[21] = "Launch the game 3 times";
        achivementString[22] = "Launch the game 5 times";
        achivementString[23] = "Launch the game 10 times";
        achivementString[24] = "Launch the game 15 times";

        achivementString[25] = "Beat your Highscore";
        achivementString[26] = "Review the game";







    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
