using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButtons : MonoBehaviour
{
    public int itemNumber;
    public InventoryScript inventoryScript;
    public ScrollCrate scrollCrate;

    [SerializeField] private GameObject borderDefault;
    [SerializeField] private GameObject borderRare;
    [SerializeField] private GameObject borderEpic;
    [SerializeField] private GameObject borderLegendary;
    [SerializeField] private GameObject filterNotOwned;
    [SerializeField] private GameObject filterEquipt;

    [SerializeField] private RawImage sprite;

    public bool disableButton = false;

    public void Update()
    {
        //if(inventoryScript.unlockedBool[itemNumber] == true)
        //{
        //    //filterNotOwned.SetActive(true);
        //}
    }

    public void Start()
    {


        SetVariables();
    }

    public void SetVariables()
    {
        //sprite = 
        //Sprite sprite1 = inventoryScript.cosmeticItemSprite[itemNumber];
        //sprite.texture = this.gameObject.GetComponentInParent<RawImage>();

        if(disableButton == true)
        {
            pickRandom();
        }

        switch (inventoryScript.cosmeticRarity[itemNumber])
        {
            case "Default":
                borderDefault.SetActive(true);
            break;
            case "Rare":
                borderRare.SetActive(true);
                break;
            case "Epic":
                borderEpic.SetActive(true);
                break;
            case "Legendary":
                borderLegendary.SetActive(true);
                break;
        }
    }
    public void onRollOver()
    {
        if (disableButton == false)
        {
            inventoryScript.rollover(itemNumber);
        }
    }

    public void pickRandom()
    {
        itemNumber = Random.Range(0, 11);
        if(inventoryScript.unlockedBool[itemNumber] == true)
        {
            pickRandom();
        }
    }
}
