using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTracker : MonoBehaviour
{
    #region Variables

    public MeshRenderer[] meshRenderer = new MeshRenderer[30]; //Mesh renderer for each of the buttons on the cockpit
    public int currentButton = 0; //The current button which is active (0-29)
    public TextMeshPro coinsText; //The text on the board behind the buttons
    public GameObject highscoreObject; //Object which appears once a new streak best has been reached.
     
    public int foundCoins = 0;
    public int streak = 0;
    public int totalCoins = 0;

    public bool onStreak = true;

    public Material collected; //Material for if it was collected (Green)
    public Material missed; //Material for if it was missed (Red)
    public Material normal; //Material before its changed (Gray)
    #endregion

    public void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        totalCoins = PlayerPrefs.GetInt("PlayerCoins"); //Get the total amount of coins the player has
        coinsText.text = "Found Coins: " + foundCoins.ToString() + "\n\n" + "Streak: " + streak.ToString() + "x" + "\n\n" + "Total Coins: " + "\n" + totalCoins.ToString();
        //Create the backing text

        //Example:

        //Found Coins: 6
        //Streak: 3x
        //Total: 4251
    }
    /// <summary>
    /// If a coin has been picked up, this function runs
    /// </summary>
    public void collectedCoin()
    {
        if (currentButton == 30) //If the script has reached the end of the list and amount of buttons
        {
            for (int i = 0; i < 30; i++) //Run through all of the buttons
            {
                meshRenderer[i].material = collected; //Set them to gray
            }
            currentButton = 0; //And reset to the start so it restarts without glitching. 
        }

        if (onStreak)
        {
            streak++; //Add 1 to the streak
            foundCoins += (1 * streak); //Times the found coins to the streak
            PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") + (1 * streak)); //Add the new coins to the player prefs
            PlayerPrefs.Save(); //Save

            Debug.Log("STREAK " + PlayerPrefs.GetFloat("storedHighestStreak"));

            if(PlayerPrefs.GetFloat("storedHighestStreak") <= streak)
            {
                PlayerPrefs.SetFloat("storedHighestStreak", streak);        
                PlayerPrefs.Save();
                highscoreObject.SetActive(true);

            }
        }
        else
        {
            onStreak = true;
            streak++;
        }
        UpdateText();
        meshRenderer[currentButton].material = collected;
        currentButton++;


    }

    public void missedCoin()
    {
        highscoreObject.SetActive(false);
        if (currentButton == 30)
        {
            for (int i = 0; i < 30; i++)
            {
                meshRenderer[i].material = collected;
            }
            currentButton = 0;
        }

        if (onStreak)
        {
            streak = 1;
            onStreak = false;
        }
        UpdateText();
        meshRenderer[currentButton].material = missed;
        currentButton++;
    }
}
