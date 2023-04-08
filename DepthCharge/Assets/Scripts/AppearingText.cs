using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppearingText : MonoBehaviour
{

    public TextMeshPro textMesh;
    public string textString;
    public float color = 1;

    private void Start()
    {
        textMesh = this.GetComponent<TextMeshPro>();
        textMesh.overrideColorTags = true;
        Debug.Log("Text spawned");
    }

    public void Update()
    {
        textMesh.text = textString.ToString();
        color = color - Time.deltaTime;

        textMesh.color = new Color32(0, 1, 0, 10);

        Debug.Log(color);

        if (color <= 0)
        {
            Debug.Log("Text DESPAWNED");
            Destroy(this.gameObject);
        }
    }
}
