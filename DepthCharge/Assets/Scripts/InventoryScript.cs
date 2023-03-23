using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class InventoryScript : MonoBehaviour
{
    [SerializeField] public string[] cosmeticItemTitle = new string[10];
    [SerializeField] public GameObject[] cosmeticItemObject = new GameObject[10];
    [SerializeField] private int[] cosmeticItemPrice = new int[10];
    [SerializeField] public bool[] unlockedBool = new bool[10];
    [SerializeField] public VideoClip[] cosmeticItemSprite = new VideoClip[10];
    [SerializeField] private string[] cosmeticItemDescription = new string[10];
    [SerializeField] public int[] cosmeticItemType = new int[10];
    [SerializeField] private int[] cosmeticItemNumber = new int[10];
    [SerializeField] public string[] cosmeticRarity = new string[10];
    [SerializeField] public Texture[] cosmeticTexture = new Texture[10];
    [SerializeField] public bool[] textureLoaded = new bool[10];

    [SerializeField] public int[] EquiptedObject = new int[3];
    [SerializeField] private Material[] mechMaterial = new Material[10];
    [SerializeField] private GameObject[] mechObject = new GameObject[5];
    [SerializeField] private Material[] additonalMechMaterial = new Material[5];


    public bool inventoryOpen;
    [SerializeField] private GameObject inventoryObject;
    [SerializeField] private Statistics statistics;

    [SerializeField] private TextMeshProUGUI rolloverTitle;
    [SerializeField] private TextMeshProUGUI rolloverDescription;
    [SerializeField] private TextMeshProUGUI rolloverPrice;
    [SerializeField] private TextMeshProUGUI dynamicButtonText;
    [SerializeField] private GameObject dynamicButtonObject;
    private int lastRollover;


    void Update()
    {
        if (inventoryOpen)
        {
            inventoryObject.SetActive(true);
        }

        if (unlockedBool[lastRollover] == true)
        {
            dynamicButtonText.text = "Equip";
        }
        else if (unlockedBool[lastRollover] == false)
        {
            dynamicButtonText.text = "Buy";
        }
    }

    public void rollover(int itemNumber)
    {
        lastRollover = itemNumber;
        rolloverTitle.text = cosmeticItemTitle[itemNumber].ToString();
        rolloverDescription.text = cosmeticItemDescription[itemNumber].ToString();
        rolloverPrice.text = cosmeticItemPrice[itemNumber].ToString();

        if (unlockedBool[itemNumber] == true)
        {
            dynamicButtonText.text = "Equip";
        }
        else if (unlockedBool[itemNumber] == false)
        {
            dynamicButtonText.text = "Buy";
        }
    }

    public void DynamicButton()
    {
        if (unlockedBool[lastRollover] == true)
        {
            EnableObject(lastRollover);
        }

        else if (unlockedBool[lastRollover] == false)
        {
            unlockedBool[lastRollover] = true;
            statistics.itemsBought++;
            
        }
    }

    public void EnableObject(int ObjectNumber)
    {
        for (int i = 1; i < 4; i++)
        {
            if (cosmeticItemType[ObjectNumber] == i)
            {
                EquiptedObject[i] = ObjectNumber;
            }
        }
        if (cosmeticItemType[ObjectNumber] == 1 || cosmeticItemType[ObjectNumber] == 3)
        {
            DespawnOfType(cosmeticItemType[ObjectNumber]);
            cosmeticItemObject[ObjectNumber].SetActive(true);

        }
        else if (cosmeticItemType[ObjectNumber] == 2)
        {
            for (int i = 0; i < 5; i++)
            {
                if (ObjectNumber != int.Parse("19"))
                {

                    mechObject[i].gameObject.GetComponent<MeshRenderer>().material = mechMaterial[ObjectNumber - 10];
                }
                else
                {
                    mechObject[i].gameObject.GetComponent<MeshRenderer>().material = additonalMechMaterial[i];
                }
            }
        }
    }

    public void DespawnOfType(int type)
    {
        for (int i = 0; i < cosmeticItemType.Length; i++)
        {
            if (cosmeticItemType[i] == type && cosmeticItemType[i] != 2)
            {
                cosmeticItemObject[i].SetActive(false);

            }
        }
    }

    public void SaveInventory()
    {

        for (int inventoryNumber = 0; inventoryNumber < 30; inventoryNumber++)
        {
            PlayerPrefs.SetString("Inventory" + inventoryNumber, unlockedBool[inventoryNumber].ToString());
        }

        //PlayerPrefs.SetString("Inventory0", unlockedBool[0].ToString());
        //PlayerPrefs.SetString("Inventory1", unlockedBool[1].ToString());
        //PlayerPrefs.SetString("Inventory2", unlockedBool[2].ToString());
        //PlayerPrefs.SetString("Inventory3", unlockedBool[3].ToString());
        //PlayerPrefs.SetString("Inventory4", unlockedBool[4].ToString());
        //PlayerPrefs.SetString("Inventory5", unlockedBool[5].ToString());
        //PlayerPrefs.SetString("Inventory6", unlockedBool[6].ToString());
        //PlayerPrefs.SetString("Inventory7", unlockedBool[7].ToString());
        //PlayerPrefs.SetString("Inventory8", unlockedBool[8].ToString());
        //PlayerPrefs.SetString("Inventory9", unlockedBool[9].ToString());
        //PlayerPrefs.SetString("Inventory10", unlockedBool[10].ToString());
        //PlayerPrefs.SetString("Inventory11", unlockedBool[11].ToString());
        //PlayerPrefs.SetString("Inventory12", unlockedBool[12].ToString());
        //PlayerPrefs.SetString("Inventory13", unlockedBool[13].ToString());
        //PlayerPrefs.SetString("Inventory14", unlockedBool[14].ToString());
        //PlayerPrefs.SetString("Inventory15", unlockedBool[15].ToString());
        //PlayerPrefs.SetString("Inventory16", unlockedBool[16].ToString());
        //PlayerPrefs.SetString("Inventory17", unlockedBool[17].ToString());
        //PlayerPrefs.SetString("Inventory18", unlockedBool[18].ToString());
        //PlayerPrefs.SetString("Inventory19", unlockedBool[19].ToString());
        //PlayerPrefs.SetString("Inventory20", unlockedBool[20].ToString());
        //PlayerPrefs.SetString("Inventory21", unlockedBool[21].ToString());
        //PlayerPrefs.SetString("Inventory22", unlockedBool[22].ToString());
        //PlayerPrefs.SetString("Inventory23", unlockedBool[23].ToString());
        //PlayerPrefs.SetString("Inventory24", unlockedBool[24].ToString());

        PlayerPrefs.SetInt("ActiveDeskToy", EquiptedObject[1]);
        PlayerPrefs.SetInt("ActiveMechColour", EquiptedObject[2]);
        PlayerPrefs.Save();
    }

    public void ReadSave()
    {
        //string[] lines = System.IO.File.ReadAllLines(path);

        //using StreamReader reader = new StreamReader(path);
        //for (int i = 0; i < unlockedBool.Length; i++)
        //{
        //    if (lines[i] == "True")
        //    {
        //        unlockedBool[i] = true;
        //    }
        //}

        for (int inventoryNumber = 0; inventoryNumber < 30; inventoryNumber++)
        {
            unlockedBool[inventoryNumber] = PlayerPrefs.GetString("Inventory" + inventoryNumber).StartsWith("T");
        }

        //    unlockedBool[0] = PlayerPrefs.GetString("Inventory0").StartsWith("T");
        //unlockedBool[1] = PlayerPrefs.GetString("Inventory1").StartsWith("T");
        //unlockedBool[2] = PlayerPrefs.GetString("Inventory2").StartsWith("T");
        //unlockedBool[3] = PlayerPrefs.GetString("Inventory3").StartsWith("T");
        //unlockedBool[4] = PlayerPrefs.GetString("Inventory4").StartsWith("T");
        //unlockedBool[5] = PlayerPrefs.GetString("Inventory5").StartsWith("T");
        //unlockedBool[6] = PlayerPrefs.GetString("Inventory6").StartsWith("T");
        //unlockedBool[7] = PlayerPrefs.GetString("Inventory7").StartsWith("T");
        //unlockedBool[8] = PlayerPrefs.GetString("Inventory8").StartsWith("T");
        //unlockedBool[9] = PlayerPrefs.GetString("Inventory9").StartsWith("T");
        //unlockedBool[10] = PlayerPrefs.GetString("Inventory10").StartsWith("T");
        //unlockedBool[11] = PlayerPrefs.GetString("Inventory11").StartsWith("T");
        //unlockedBool[12] = PlayerPrefs.GetString("Inventory12").StartsWith("T");
        //unlockedBool[13] = PlayerPrefs.GetString("Inventory13").StartsWith("T");
        //unlockedBool[14] = PlayerPrefs.GetString("Inventory14").StartsWith("T");
        //unlockedBool[15] = PlayerPrefs.GetString("Inventory15").StartsWith("T");
        //unlockedBool[16] = PlayerPrefs.GetString("Inventory16").StartsWith("T");
        //unlockedBool[17] = PlayerPrefs.GetString("Inventory17").StartsWith("T");
        //unlockedBool[18] = PlayerPrefs.GetString("Inventory18").StartsWith("T");
        //unlockedBool[19] = PlayerPrefs.GetString("Inventory19").StartsWith("T");
        //unlockedBool[20] = PlayerPrefs.GetString("Inventory20").StartsWith("T");
        //unlockedBool[21] = PlayerPrefs.GetString("Inventory21").StartsWith("T");
        //unlockedBool[22] = PlayerPrefs.GetString("Inventory22").StartsWith("T");
        //unlockedBool[23] = PlayerPrefs.GetString("Inventory23").StartsWith("T");
        //unlockedBool[24] = PlayerPrefs.GetString("Inventory24").StartsWith("T");






        EnableObject(PlayerPrefs.GetInt("ActiveDeskToy"));
        EnableObject(PlayerPrefs.GetInt("ActiveMechColour"));

        //if (lines[30] != "null")
        //{
        //    EnableObject(int.Parse(lines[30]));     
        //    Debug.Log(lines[30]);
        //}
        //if (lines[31] != "null")
        //{
        //    EnableObject(int.Parse(lines[31]));   
        //    Debug.Log(lines[31]);
        //}
    }

    //public void ResetSave()
    //{
    //    File.WriteAllText(path, string.Empty);

    //    StreamWriter writer = new StreamWriter(path, true);

    //    for (int i = 0; i < unlockedBool.Length; i++)
    //    {
    //        writer.WriteLine("False");
    //    }

    //    for (int j = 1; j < 3; j++)
    //    {
    //        writer.WriteLine(-1);
    //    }

    //    writer.Close();
    //}
}
