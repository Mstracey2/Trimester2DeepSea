using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageBarScript : MonoBehaviour
{

    public GameObject movingBar; //The part of the bar which moves
    public GameObject staticBar; //The background of the percentage bar

    public Material green; 
    public Material orange;
    public Material red;

    public float maxInput; 
    public float currentInput;

    public float percentage;

    void Update()
    {
        percentage = currentInput / maxInput; //Calculate the percentage

        if (percentage < 0) //If smaller than 0...
        {
            percentage = 0; //Set to 0 to stop visual bugs
            movingBar.gameObject.SetActive(false); //And hide the moving bar
        }
        else //Else, its bigger than 0
        {
            movingBar.gameObject.SetActive(true); //Turn on the moving bar again
        }
        
        if (percentage > 100) //If the percentage somehow surpasses 100...
        {
            percentage = 100; //Set back to 100 to stop visual bugs
        }

        movingBar.gameObject.transform.localScale = new Vector3(percentage,1,1); //Set the moving bar to the current percentage
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
