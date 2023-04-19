using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenCrateFromMenu : MonoBehaviour
{
    public GameObject crateButton;
    public TextMeshProUGUI crateCountText;
    private void Update()
    {
        if(PlayerPrefs.GetInt("LootcratesHolding") >= 1)
        {
            crateButton.SetActive(true);
            crateCountText.text = PlayerPrefs.GetInt("LootcratesHolding").ToString("0");
        }
        else
        {
            crateButton.SetActive(false);
        }
    }
}
