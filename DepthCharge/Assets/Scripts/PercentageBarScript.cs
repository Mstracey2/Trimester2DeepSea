using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageBarScript : MonoBehaviour
{

    public GameObject movingBar;
    public GameObject staticBar;

    public Material green;
    public Material orange;
    public Material red;

    public float maxInput;
    public float currentInput;

    public float percentage;

    void Update()
    {
        percentage = currentInput / maxInput;

        if (percentage < 0)
        {
            percentage = 0;
            movingBar.gameObject.SetActive(false);
        }
        else
        {
            movingBar.gameObject.SetActive(true);
        }
        
        if (percentage > 100)
        {
            percentage = 100;
        }

       

        movingBar.gameObject.transform.localScale = new Vector3(percentage,1,1);
    }

    public void changeColour(string Colour)
    {
        switch (Colour)
        {
            case "green":
                movingBar.GetComponent<MeshRenderer>().material = green;
                break;

            case "orange":
                movingBar.GetComponent<MeshRenderer>().material = orange;
                break;

            case "red":
                movingBar.GetComponent<MeshRenderer>().material = red;
                break;
        } 
    }
}
