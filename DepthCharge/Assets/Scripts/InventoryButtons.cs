using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtons : MonoBehaviour
{
    public int itemNumber;
    public InventoryScript inventoryScript;

    public void onRollOver() 
    {

        inventoryScript.rollover(itemNumber);
    } 
}
