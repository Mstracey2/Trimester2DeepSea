
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;
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

    private int deskToyEnabled;


    public bool inventoryOpen;
    [SerializeField] private GameObject inventoryObject;

    [SerializeField] private TextMeshProUGUI rolloverTitle;
    [SerializeField] private TextMeshProUGUI rolloverDescription;
    [SerializeField] private TextMeshProUGUI rolloverPrice;
    [SerializeField] private TextMeshProUGUI dynamicButtonText;
    [SerializeField] private GameObject dynamicButtonObject;
    private int lastRollover;
    // [SerializeField] private RawImageEditor rolloverPicture;



    public string path = "Assets/Saves/Inventory.txt";

    void Update()
    {


        if (inventoryOpen)
        {
            inventoryObject.SetActive(true);
        }

        if (unlockedBool[lastRollover] == true)
        {
            dynamicButtonText.text = "Equipt";
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
        //rolloverPicture. = cosmeticItemSprite[itemNumber];


        if (unlockedBool[itemNumber] == true)
        {
            dynamicButtonText.text = "Equipt";
        }
        else if (unlockedBool[itemNumber] == false)
        {
            dynamicButtonText.text = "Buy";
        }
    }

    public void DynamicButton()
    {
        //Add something to check player has enough moneyz

        if (unlockedBool[lastRollover] == true)
        {
            DespawnOfType(cosmeticItemType[lastRollover]);
            cosmeticItemObject[lastRollover].SetActive(true);
        }

        else if (unlockedBool[lastRollover] == false)
        {
            unlockedBool[lastRollover] = true;
        }
    }

    public void DespawnOfType(int type)
    {
        for (int i = 0; i < cosmeticItemType.Length; i++)
        {
            if (cosmeticItemType[i] == type)
            {
                cosmeticItemObject[i].SetActive(false);
                deskToyEnabled = i;
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
        writer.Close();
    }

    public void ReadSave()
    {
        string[] lines = System.IO.File.ReadAllLines(path);

        using StreamReader reader = new StreamReader(path);
        for (int i = 0; i < unlockedBool.Length; i++)
        {
           if(lines[i] == "True")
            {
                unlockedBool[i] = true;
            } 
        }
    }
}
