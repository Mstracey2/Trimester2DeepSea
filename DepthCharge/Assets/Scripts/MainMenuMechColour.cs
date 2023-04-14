using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMechColour : MonoBehaviour
{
    [SerializeField] private Material[] mechMaterial = new Material[10];
    [SerializeField] private GameObject[] mechObject = new GameObject[5];
    [SerializeField] private Material[] additonalMechMaterial = new Material[5];


    void Start()
    {
        Time.timeScale = 1;

        if (PlayerPrefs.GetInt("ActiveMechColour") != 0)
        {
            EnableObject(PlayerPrefs.GetInt("ActiveMechColour"));
        }
        PlayerPrefs.SetInt("savedTimesLaunched", PlayerPrefs.GetInt("savedTimesLaunched") + 1);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Function runs when the player enables an object
    /// </summary>
    /// <param name="ObjectNumber"></param>
    public void EnableObject(int ObjectNumber)
    {
        for (int i = 0; i < 5; i++)
        {
            if (ObjectNumber != int.Parse("19"))  //And if the number isn't 19
            {
                mechObject[i].gameObject.GetComponent<MeshRenderer>().material = mechMaterial[ObjectNumber - 10]; //Simply set the material to the correct material on all 5 objects
            }
            else //If it is 19 (Because 19 has multiple different colours)
            {
                mechObject[i].gameObject.GetComponent<MeshRenderer>().material = additonalMechMaterial[i]; //Set each limb to the sepereate correct colour
            }
        }
    }
}

