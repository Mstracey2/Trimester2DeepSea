using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Statistics : MonoBehaviour
{
    public string path = "Assets/Saves/Statistics.txt";

    public float runs;
    public float itemsOwned;
    public float playtimeSeconds;
    public float boxesOpened;
    public float itemsBought;
    public float timesLaunched;
    public float timesReviewed;

    public float experience;

    public List<string> listOfStats = new List<string>(10);


    public void Start()
    {
        loadStats();
    }

    void Update()
    {       
        listOfStats[1] = GameManager.currentManager.experienceFloat.ToString();
        listOfStats[2] = runs.ToString();
        listOfStats[3] = playtimeSeconds.ToString();
        listOfStats[4] = boxesOpened.ToString();
        listOfStats[5] = itemsBought.ToString();
        listOfStats[6] = timesLaunched.ToString();
        listOfStats[7] = timesReviewed.ToString();
       
    }

    public void saveStats()
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < 10; i++)
        {
            writer.WriteLine(listOfStats[i]);
        }
        writer.Close();
    }

    public void loadStats()
    {
        string[] lines = System.IO.File.ReadAllLines(path);

        using StreamReader reader = new StreamReader(path);


      GameManager.currentManager.experienceFloat = float.Parse(lines[1]);
        runs = float.Parse(lines[2]);
        playtimeSeconds = float.Parse(lines[3]);
        boxesOpened = float.Parse(lines[4]);
        itemsBought = float.Parse(lines[5]);
        timesLaunched = float.Parse(lines[6]);
        timesReviewed = float.Parse(lines[7]);
    }

    public void ResetStats()
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < 10; i++)
        {
            writer.WriteLine(0);
        }
        writer.Close();
    }
}

