using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class Statistics : MonoBehaviour
{

    public string path = "Assets/Saves/Statistics.txt";

    public int runs;
    public int itemsOwned;
    public int playtimeSeconds;

    public GameManager gameManager;

    public float experience;

    public List<string> listOfStats = new List<string>(3);

    void Update()
    {
        listOfStats[1] = gameManager.experienceFloat.ToString();
        loadStats();
        
    }

    public void saveStats()
    {
        File.WriteAllText(path, string.Empty);
        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < listOfStats.Count; i++)
        {
            writer.WriteLine(listOfStats[i]);
        }
        writer.Close();
    }

    public void loadStats()
    {
        string[] lines = System.IO.File.ReadAllLines(path);

        using StreamReader reader = new StreamReader(path);


        gameManager.experienceFloat = float.Parse(lines[1]);
    }
}

