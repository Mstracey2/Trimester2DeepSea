using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    public int itemNumber;
    public InventoryScript inventoryScript;

    private GameObject borderDefault;
    private GameObject borderEpic;
    private GameObject borderLegendary;
    private GameObject filterNotOwned;
    private GameObject filterEquipt;

    public void Update()
    {
    //    if(inventoryScript.)
    }

    public void Start()
    {
        
        //if(inventoryScript.cosmeticRarity)
        switch (inventoryScript.cosmeticRarity[itemNumber])
        {
            case "Default":
              //  borderDefault = this.g
            break;
        }
            
            
    }
    public void onRollOver() 
    {

        inventoryScript.rollover(itemNumber);
    } 
}
