using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.UI;

public class sceneManager : MonoBehaviour
{
    public string Link = "https://forms.gle/3e5T895P6ky8nhXQA";
    public void playGame()
    {
        SceneManager.LoadScene("Scene");
    }
    public void exitGame()
    {
        Application.Quit();
    }

    public void setToClipboard()
    {
        GUIUtility.systemCopyBuffer = Link;
    }
}
