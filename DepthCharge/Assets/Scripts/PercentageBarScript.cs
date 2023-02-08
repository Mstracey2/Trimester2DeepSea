using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageBarScript : MonoBehaviour
{

    public GameObject movingBar;
    public GameObject staticBar;

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
}
