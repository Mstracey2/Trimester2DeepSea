using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class InventoryScript : MonoBehaviour
{

    #region Variables
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


    public bool inventoryOpen; //If the player has the inventory open...
    [SerializeField] private GameObject inventoryObject; //The canvas for the inventory
    [SerializeField] private Statistics statistics; //Reference to the statstics script

    [SerializeField] private TextMeshProUGUI rolloverTitle; //Where the title is displayed if clicked on...
    [SerializeField] private TextMeshProUGUI rolloverDescription; //Where the description is displayed if clicked on...
    [SerializeField] private TextMeshProUGUI rolloverPrice; //Where the price is displayed if clicked on...
    [SerializeField] private TextMeshProUGUI dynamicButtonText; //Button text, will either display Buy, Equip or Insuffient Funds based on player factors
    [SerializeField] private GameObject dynamicButtonObject; //The button object

    [SerializeField] private TextMeshProUGUI playersCoins; //Where the number of coins the player has is displayed
    private int lastRollover; //The item number of the last clicked on number, this decides what info is shown

    #endregion

    private void Start()
    {
        ReadSave();
    }

    void Update()
    {
        playersCoins.text = PlayerPrefs.GetInt("PlayerCoins").ToString(); //Set the player coins text
        if (inventoryOpen)
        {
            inventoryObject.SetActive(true); //Turn on the Inventory canvas
        }

        if (unlockedBool[lastRollover] == true) //If the player owns the clicked on item
        {
            dynamicButtonText.text = "Equip"; //Display Equip
        }

        else if (unlockedBool[lastRollover] == false) //If the player doesn't have the item...
        {
            if (cosmeticItemPrice[lastRollover] <= PlayerPrefs.GetInt("PlayerCoins")) //And if the player has enough money...
            {
                dynamicButtonText.text = "Buy"; //Display Buy
            }
            else //Else, the player doesn't have enough money
            {
                dynamicButtonText.text = "Insufficent Funds";  //Display Insufficent Funds
            }
        }
    }
    /// <summary>
    /// When the player clicks on an item, this function runs and passes through the items number
    /// </summary>
    /// <param name="itemNumber"></param>
    public void rollover(int itemNumber)
    {
        lastRollover = itemNumber; //Set the lastRollover
        rolloverTitle.text = cosmeticItemTitle[itemNumber].ToString(); //And the Title
        rolloverDescription.text = cosmeticItemDescription[itemNumber].ToString(); //Description
        rolloverPrice.text = cosmeticItemPrice[itemNumber].ToString(); //And price...

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

    /// <summary>
    /// Button at the bottom of the shop which changes function based on how much money the player has and if they own the item yet
    /// </summary>
    public void DynamicButton()
    {
        if (unlockedBool[lastRollover] == true) //If the player has it
        {
            EnableObject(lastRollover); //Let the player equip it
        }

        else if (unlockedBool[lastRollover] == false)// If the player doesn't own it yet...
        {
            if (cosmeticItemPrice[lastRollover] <= PlayerPrefs.GetInt("PlayerCoins")) //And if the player has enough money
            {
                unlockedBool[lastRollover] = true; //The player now owns that item
                PlayerPrefs.SetInt("PlayerCoins", PlayerPrefs.GetInt("PlayerCoins") - cosmeticItemPrice[lastRollover]); //Calculate the new player coin total
                playersCoins.text = PlayerPrefs.GetInt("PlayerCoins").ToString(); //Update the text
                statistics.itemsBought++; //Add to how many items the player has bought
                statistics.saveStats(); //Save that information
                SaveInventory(); //And save the new inventory information
            }
        }
    }
    /// <summary>
    /// Function runs when the player enables an object
    /// </summary>
    /// <param name="ObjectNumber"></param>
    public void EnableObject(int ObjectNumber)
    {
        for (int i = 1; i < 4; i++)
        {
            if (cosmeticItemType[ObjectNumber] == i) //If the same item type is ran, (1 = Desk and 2 = Mech Colour)
            {
                EquiptedObject[i] = ObjectNumber; 
            }
        }
        if (cosmeticItemType[ObjectNumber] == 1 || cosmeticItemType[ObjectNumber] == 3) //If the item type is a desk toy
        {
            DespawnOfType(cosmeticItemType[ObjectNumber]); //Make sure there aren't any other desk toys active anymore
            cosmeticItemObject[ObjectNumber].SetActive(true); //And enable the selected item

        }
        else if (cosmeticItemType[ObjectNumber] == 2) //If the type is a colour
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
    /// <summary>
    /// Despawns all items of that type
    /// </summary>
    /// <param name="type"></param>
    public void DespawnOfType(int type)
    {
        for (int i = 0; i < cosmeticItemType.Length; i++)
        {
            if (cosmeticItemType[i] == type && cosmeticItemType[i] != 2) //All of the same type, if not 2 (So only desk toy)
            {
                cosmeticItemObject[i].SetActive(false); //Set inactive to not overlap

            }
        }
    }

    /// <summary>
    /// Save the current state of the inventory to PlayerPrefs
    /// </summary>
    public void SaveInventory()
    {
        for (int inventoryNumber = 0; inventoryNumber < 30; inventoryNumber++) //Run for each inventory item
        {
            PlayerPrefs.SetString("Inventory" + inventoryNumber, unlockedBool[inventoryNumber].ToString()); //Set to True or False for if unlocked 
        }

        PlayerPrefs.SetInt("ActiveDeskToy", EquiptedObject[1]); //Save what item the player currently has displayed on their cockpit
        PlayerPrefs.SetInt("ActiveMechColour", EquiptedObject[2]); //Save what colour the player currently has as their mech
        PlayerPrefs.Save();
    }

    public void ReadSave()
    {
        for (int inventoryNumber = 0; inventoryNumber < 30; inventoryNumber++) //Run for each inventory item
        {
            unlockedBool[inventoryNumber] = PlayerPrefs.GetString("Inventory" + inventoryNumber).StartsWith("T"); //If it is true
        }

        if (string.IsNullOrEmpty((PlayerPrefs.GetInt("ActiveDeskToy").ToString()))) //If not empty
        {

        }
        else
        {
            EnableObject(PlayerPrefs.GetInt("ActiveDeskToy")); //Set the same item back to active
        }

        {

        }
        EnableObject(PlayerPrefs.GetInt("ActiveMechColour"));

    }
}
