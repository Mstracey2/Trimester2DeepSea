using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor.UI;
public class InventoryScript : MonoBehaviour
{
    [SerializeField] private string[] cosmeticItemTitle = new string[10];
    [SerializeField] private GameObject[] cosmeticItemObject = new GameObject[10];
    [SerializeField] private int[] cosmeticItemPrice = new int[10];
    [SerializeField] private bool[] unlockedBool = new bool[10];
    [SerializeField] private Sprite[] cosmeticItemSprite = new Sprite[10];
    [SerializeField] private string[] cosmeticItemDescription = new string[10];
    [SerializeField] private int[] cosmeticItemType = new int[10];
    [SerializeField] private int[] cosmeticItemNumber = new int[10];

    public bool inventoryOpen;
    [SerializeField] private GameObject inventoryObject;

    [SerializeField] private TextMeshProUGUI rolloverTitle;
    [SerializeField] private TextMeshProUGUI rolloverDescription;
    [SerializeField] private TextMeshProUGUI rolloverPrice;
    [SerializeField] private TextMeshProUGUI dynamicButtonText;
    [SerializeField] private GameObject dynamicButtonObject;
    private int lastRollover;
   // [SerializeField] private RawImageEditor rolloverPicture;




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
        //
        
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

        if(unlockedBool[lastRollover] == true)
        {
            DespawnOfType(cosmeticItemType[lastRollover]);
            cosmeticItemObject[lastRollover].SetActive(true);
        }

        else if(unlockedBool[lastRollover] == false)
        {
            unlockedBool[lastRollover] = true;
        }
    }

    public void DespawnOfType(int type)
    {
        for (int i = 0; i < cosmeticItemType.Length; i++)
        {
            if(cosmeticItemType[i] == type)
            {
                cosmeticItemObject[i].SetActive(false);
            }
        }
    }
}
