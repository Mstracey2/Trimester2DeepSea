using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AppearingText : MonoBehaviour
{

    public TextMeshPro text;
    public float color = 255;

    private void Start()
    {
        text = this.GetComponent<TextMeshPro>();
        Debug.Log("Text spawned");
    }

    public void Update()
    {
        color = color - (Time.deltaTime * 100);

        text.color = new Vector4(0, 255, 19, color);

        if (color <= 0)
        {
            Debug.Log("Text DESPAWNED");
            Destroy(this);
        }
    }
}
