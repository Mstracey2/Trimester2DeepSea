using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppearingText : MonoBehaviour
{
    public TextMeshPro textMesh; // The text object
    public string textString; //What should be displayed
    public float color = 1; //The transparency of the object

    private void Start()
    {
        textMesh = this.GetComponent<TextMeshPro>();
        textMesh.overrideColorTags = true; //Allow the alpha to be changed
    }

    public void Update()
    {
        textMesh.text = textString.ToString(); //Set the text
        color = color - Time.deltaTime; //Count down with the Alpha

        textMesh.color = new Color32(0, 1, 0, 10); //Set the colour

        if (color <= 0)//If the alpha reaches 0
        {
            Destroy(this.gameObject); //Destroy the object.
        }
    }
}
