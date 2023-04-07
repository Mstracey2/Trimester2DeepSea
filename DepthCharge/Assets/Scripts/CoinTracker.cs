 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinTracker : MonoBehaviour
{

    public MeshRenderer[] meshRenderer = new MeshRenderer[30];
    public int currentButton = 0;
    public TextMeshPro coinsText;

    public int foundCoins = 0;
    public int streak = 0;
    public int totalCoins = 0;

    public bool onStreak = true;

    public Material collected;
    public Material missed;
    public Material normal;


    public void Start()
    {
        totalCoins = PlayerPrefs.GetInt("PlayerCoins");
        UpdateText();
       
    }

    public void UpdateText()
    {

        coinsText.text = "Found Coins: " + foundCoins.ToString() + "\n\n" + "Streak: " + streak.ToString() + "x" + "\n\n" + "Total Coins: " + totalCoins.ToString(); 

    }

    public void collectedCoin()
    {     
        if(currentButton == 30)
        {
            for (int i = 0; i < 30; i++)
            {
                meshRenderer[i].material = collected;
            }
            currentButton = 0;
        }

        if (onStreak)
        {
            streak++;
            foundCoins += (1 * streak);
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
            streak = 0;
            onStreak = false;
        }
        UpdateText();
        meshRenderer[currentButton].material = missed;
        currentButton++;
    }
}
