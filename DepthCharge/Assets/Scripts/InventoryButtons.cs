using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    public int itemNumber;
    public InventoryScript inventoryScript;

    [SerializeField] private GameObject borderDefault;
    [SerializeField] private GameObject borderRare;
    [SerializeField] private GameObject borderEpic;
    [SerializeField] private GameObject borderLegendary;
    [SerializeField] private GameObject filterNotOwned;
    [SerializeField] private GameObject filterEquipt;

    public void Update()
    {
        if(inventoryScript.unlockedBool[itemNumber] == true)
        {
            //filterNotOwned.SetActive(true);
        }
    }

    public void Start()
    {


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

        inventoryScript.rollover(itemNumber);
    } 
}
