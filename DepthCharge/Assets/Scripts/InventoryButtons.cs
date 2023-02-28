using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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
    private VideoPlayer videoPlayer;
    private bool randomObject;

    [SerializeField] private RawImage sprite;

    public bool disableButton = false;

    public void Update()
    {

    }

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
        this.GetComponent<VideoPlayer>().targetTexture = (RenderTexture)inventoryScript.cosmeticTexture[itemNumber];
        this.GetComponent<RawImage>().texture = inventoryScript.cosmeticTexture[itemNumber];
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
        itemNumber = Random.Range(1, 30);
        if (inventoryScript.unlockedBool[itemNumber] == true)
        {
            pickRandom();
        }
    }


    private IEnumerator PickRandomObject()
    {
        randomObject = false;

        while(randomObject == false)
        {
            itemNumber = Random.Range(1, 30);
            if(inventoryScript.unlockedBool[itemNumber] == false)
            {
                randomObject = true;
            }
        }

        yield return null;

    }
}
