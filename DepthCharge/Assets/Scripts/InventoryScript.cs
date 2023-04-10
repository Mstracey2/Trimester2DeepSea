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

    [SerializeField] private TextMeshProUGUI playersCoins;
    private int lastRollover;


    private void Start()
    {
        ReadSave();
    }

    void Update()
    {
        playersCoins.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
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
            if (cosmeticItemPrice[lastRollover] <= PlayerPrefs.GetInt("PlayerCoins"))
            {
                dynamicButtonText.text = "Buy";
            }
            else
            {
                dynamicButtonText.text = "Insufficent Funds";
            }

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
            if (cosmeticItemPrice[itemNumber] >= PlayerPrefs.GetInt("PlayerCoins"))
            {
                dynamicButtonText.text = "Buy";
            }
            else
            {
                dynamicButtonText.text = "Insufficent Funds";
            }

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
            if (cosmeticItemPrice[lastRollover] <= PlayerPrefs.GetInt("PlayerCoins"))
            {
                unlockedBool[lastRollover] = true;
                PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") - cosmeticItemPrice[lastRollover]);
                playersCoins.text = PlayerPrefs.GetInt("PlayerCoins").ToString();
                Debug.Log("RAN");
                statistics.itemsBought++;
                statistics.saveStats();
                SaveInventory();
            }
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

        PlayerPrefs.SetInt("ActiveDeskToy", EquiptedObject[1]);
        PlayerPrefs.SetInt("ActiveMechColour", EquiptedObject[2]);
        PlayerPrefs.Save();
    }

    public void ReadSave()
    {
        for (int inventoryNumber = 0; inventoryNumber < 30; inventoryNumber++)
        {
            unlockedBool[inventoryNumber] = PlayerPrefs.GetString("Inventory" + inventoryNumber).StartsWith("T");
        }

        if (string.IsNullOrEmpty((PlayerPrefs.GetInt("ActiveDeskToy").ToString())))
        {

        }
        else
        {
            EnableObject(PlayerPrefs.GetInt("ActiveDeskToy"));
        }

        {

        }
        EnableObject(PlayerPrefs.GetInt("ActiveMechColour"));

    }
}
