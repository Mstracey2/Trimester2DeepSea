using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class InventoryButtons : MonoBehaviour
{
    public int itemNumber; //The item number of this current button, set in the inspector
    public InventoryScript inventoryScript; //A reference to the inventory manager
    public ScrollCrate scrollCrate; //A reference to the lootcrate manager

    [SerializeField] private GameObject borderDefault; //A reference to the border, depending on the set int it will enable the correct one
    [SerializeField] private GameObject borderRare;
    [SerializeField] private GameObject borderEpic;
    [SerializeField] private GameObject borderLegendary;
    [SerializeField] private GameObject filterNotOwned;
    [SerializeField] private GameObject filterEquipt;

    private VideoPlayer videoPlayer; 
    private bool randomObject; //Bool decides if this script is required to randomise its item number, used for the lootcrate so the same object can be used for loot crates and inventory

    [SerializeField] private RawImage sprite;

    public bool disableButton = false; //If the button should be clickable or not.

    public void Start()
    {
        SetVariables();
    }

    public void SetVariables()
    {
        if (disableButton == true)
        {
            StartCoroutine(PickRandomObject());
            StopCoroutine(PickRandomObject());
        }

        videoPlayer = this.GetComponent<VideoPlayer>();

        if (inventoryScript.textureLoaded[itemNumber] == false)
        {
            videoPlayer.clip = inventoryScript.cosmeticItemSprite[itemNumber];
            inventoryScript.textureLoaded[itemNumber] = true;
        }
        this.GetComponent<VideoPlayer>().targetTexture = (RenderTexture)inventoryScript.cosmeticTexture[itemNumber]; //Get the MP4 file from the manager and set it to itself
        this.GetComponent<RawImage>().texture = inventoryScript.cosmeticTexture[itemNumber]; //Set the texture 
      
        switch (inventoryScript.cosmeticRarity[itemNumber])
        {
            case "Default":                  //If default...
                borderDefault.SetActive(true); //Enable the default border on this object
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
    /// <summary>
    /// If the player clicks on the object, tell the manager it has been selected
    /// </summary>
    public void onRollOver() 
    {
        if (disableButton == false) //If the button is clickable...
        {
            inventoryScript.rollover(itemNumber); //Pass on the info
        }
    }

    /// <summary>
    /// If this item is on the lootcrate it will generate a random number
    /// </summary>
    public void pickRandom()
    {
        itemNumber = Random.Range(0, 19); //Pick a random number between 0 and 19 (All possible cosmetics)
        if (inventoryScript.unlockedBool[itemNumber] == true) //If it has already been collected...
        {
            pickRandom(); //Pick random again
        } //This will continue as many times thats required until it picks an item the player hasn't got yet
    }


    private IEnumerator PickRandomObject()
    {
        randomObject = false;

        while(randomObject == false)
        {
            itemNumber = Random.Range(0, 19);
            if(inventoryScript.unlockedBool[itemNumber] == false)
            {
                randomObject = true;
            }
        }

        yield return null;

    }
}
