using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DebugCodes : MonoBehaviour
{
    public TMP_InputField field;
    public string text;

    public TextMeshProUGUI result;

    public void Check()
    {
        text = field.text.ToLower();

        if (text == "motherlode")
        {
            result.text = "+5000 Coins";
            PlayerPrefs.SetInt("PlayerCoins", 5000);
            PlayerPrefs.Save();
        }

        else if(text == "lootcrates")
        {
            result.text = "+10 Lootcrates";
            PlayerPrefs.SetInt("LootcratesHolding", 10);
            PlayerPrefs.Save();
        }
           
        else
        {
            result.text = "Code Not Found";
        }
    }
}
