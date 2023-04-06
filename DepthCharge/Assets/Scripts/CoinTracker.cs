 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTracker : MonoBehaviour
{
  //  public GameObject[] buttons = new GameObject[30];
    public MeshRenderer[] meshRenderer = new MeshRenderer[30];
    public int currentButton = 0;
   // public MeshRenderer meshRenderer;

    public Material collected;
    public Material missed;
    public Material normal;



    public void collectedCoin()
    {     
        if(currentButton == 30)
        {
            for (int i = 0; i < 30; i++)
            {
                meshRenderer[i].material = collected;
            }
            currentButton = 0;
        }
        meshRenderer[currentButton].material = collected;
        currentButton++;


    }

    public void missedCoin()
    {
        if (currentButton == 30)
        {
            for (int i = 0; i < 30; i++)
            {
                meshRenderer[i].material = collected;
            }
            currentButton = 0;
        }
        meshRenderer[currentButton].material = missed;
        currentButton++;
    }
}
