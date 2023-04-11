using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppearingText : MonoBehaviour
{
    public TextMeshPro textMesh; // The text object
    public string textString; //What should be displayed
    public float color = 1; //The transparency of the object
    private float alpha;

    private void Start()
    {
        textMesh = this.GetComponent<TextMeshPro>();
        textMesh.overrideColorTags = true; //Allow the alpha to be changed
        alpha = 1.0f;
    }

    public void Update()
    {
        textMesh.text = textString.ToString(); //Set the text
        color = color - Time.deltaTime; //Count down with the Alpha

        alpha -= Time.deltaTime / 2;
        // Clamp alpha value between 0 and 1
        alpha = Mathf.Clamp01(alpha);
        // Set the text mesh color with the new alpha value
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alpha);

     //   textMesh.color = new Color32(0, 1, 0, 10); //Set the colour

        if (color <= 0)//If the alpha reaches 0
        {
            Destroy(this.gameObject); //Destroy the object.
        }
    }
}
