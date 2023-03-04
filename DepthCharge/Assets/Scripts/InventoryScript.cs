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


    public GameManager gameManager;

    public bool inventoryOpen;
    [SerializeField] private GameObject inventoryObject;
    [SerializeField] private Statistics statistics;

    [SerializeField] private TextMeshProUGUI rolloverTitle;
    [SerializeField] private TextMeshProUGUI rolloverDescription;
    [SerializeField] private TextMeshProUGUI rolloverPrice;
    [SerializeField] private TextMeshProUGUI dynamicButtonText;
    [SerializeField] private GameObject dynamicButtonObject;
    private int lastRollover;

    public string path = "Assets/Saves/Inventory.txt";

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
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < unlockedBool.Length; i++)
        {
            writer.WriteLine(unlockedBool[i]);
        }

        for (int j = 1; j < 3; j++)
        {
            writer.WriteLine(EquiptedObject[j]);
        }

            writer.Close();
    }

    public void ReadSave()
    {
        string[] lines = System.IO.File.ReadAllLines(path);

        using StreamReader reader = new StreamReader(path);
        for (int i = 0; i < unlockedBool.Length; i++)
        {
            if (lines[i] == "True")
            {
                unlockedBool[i] = true;
            }
        }

        if (lines[30] != "null")
        {
            EnableObject(int.Parse(lines[30]));
        }
        if (lines[31] != "null")
        {
            EnableObject(int.Parse(lines[31]));
        }
    }

    public void ResetSave()
    {
        File.WriteAllText(path, string.Empty);

        StreamWriter writer = new StreamWriter(path, true);

        for (int i = 0; i < unlockedBool.Length; i++)
        {
            writer.WriteLine("False");
        }

        for (int j = 1; j < 3; j++)
        {
            writer.WriteLine(-1);
        }

        writer.Close();
    }
}
