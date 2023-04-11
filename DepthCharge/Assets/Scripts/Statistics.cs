using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Statistics : MonoBehaviour
{
    public string path;

    public float runs;
    public float itemsOwned;
    public float playtimeSeconds;
    public float boxesOpened;
    public float itemsBought;
    public float timesLaunched;
    public float timesReviewed;
    public float highestStreak;

    public float experience;

    public List<string> listOfStats = new List<string>(10);

    public void Awake()
    {
      loadStats(); //Load all stats before everything else...
    }

    void Update()
    {       
        //Make a list of all the stats that are required to be saved
        listOfStats[1] = GameManager.currentManager.experienceFloat.ToString();
        listOfStats[2] = runs.ToString();
        listOfStats[3] = playtimeSeconds.ToString();
        listOfStats[4] = boxesOpened.ToString();
        listOfStats[5] = itemsBought.ToString();
        listOfStats[6] = timesLaunched.ToString();
        listOfStats[7] = timesReviewed.ToString();
        listOfStats[8] = highestStreak.ToString();
       
    }
    /// <summary>
    /// Save all the information to PlayerPrefs
    /// </summary>
    public void saveStats()
    {
        PlayerPrefs.SetFloat("savedExperience", GameManager.currentManager.experienceFloat);
        PlayerPrefs.SetFloat("savedRuns", runs);
        PlayerPrefs.SetFloat("savedPlaytime", playtimeSeconds);
        PlayerPrefs.SetFloat("savedBoxesOpened", boxesOpened);
        PlayerPrefs.SetFloat("savedItemsBought", itemsBought);
        PlayerPrefs.SetFloat("savedTimesLaunched", timesLaunched);
        PlayerPrefs.SetFloat("savedReviews", timesReviewed);
        PlayerPrefs.SetFloat("storedHighestStreak", highestStreak);
        PlayerPrefs.Save();
    }
    
    /// <summary>
    /// Get all the information from PlayerPrefs and save it 
    /// </summary>
    public void loadStats()
    {
        GameManager.currentManager.experienceFloat = PlayerPrefs.GetFloat("savedExperience");
        runs = PlayerPrefs.GetFloat("savedRuns");
        playtimeSeconds = PlayerPrefs.GetFloat("savedPlaytime");
        boxesOpened = PlayerPrefs.GetFloat("savedBoxesOpened");
        itemsBought = PlayerPrefs.GetFloat("savedItemsBought");
        timesLaunched = PlayerPrefs.GetFloat("savedTimesLaunched");
        timesReviewed = PlayerPrefs.GetFloat("savedReviews");
        highestStreak = PlayerPrefs.GetFloat("storedHighestStreak");
        Debug.Log(PlayerPrefs.GetFloat("savedExperience"));
    }

}

