using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPercentage : MonoBehaviour
{
    [SerializeField] private int rangeMin;
    [SerializeField] private int rangeMax;
    [SerializeField] private GameObject scaleObject;
    public InventoryScript inventoryScript;
    public float number;
    [SerializeField] private float percentage;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        number = 0;

        for (int i = rangeMin; i < rangeMax; i++)
        {
            if (inventoryScript.unlockedBool[i] == true)
            {
                number++;
                percentage = number / rangeMax;
                scaleObject.gameObject.transform.localScale = new Vector2 (percentage, 1);
            }
        }
    }  
}
