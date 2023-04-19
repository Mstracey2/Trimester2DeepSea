using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class sceneManager : MonoBehaviour
{
    public string Link = "https://forms.gle/3e5T895P6ky8nhXQA";
    public GameObject mech;
    public Transform waypoint1;
    public Transform waypoint2;
    public float launch = 0f;
    public GameObject menuObjects;
    [SerializeField] private Statistics statistics;
    [SerializeField] private InventoryScript saveInventory;
    [SerializeField] private Statistics saveStatistics;
    [SerializeField] private AchivementsManager achivementsManager;

    private void Start()
    {
        FindObjectOfType<AudioManager>().Play("Menu Music");
    }

    private void Update()
    {
        // If launch has started, go to first waypoint
        if (launch == 1f)
        {
            var step = 25f * Time.deltaTime;
            mech.transform.position = Vector3.MoveTowards(mech.transform.position, waypoint1.position, step);

            if (mech.transform.position == waypoint1.position)
            {
                launch = 2f;
            }
        }

        // If first waypoint was hit, go to second waypoint, then start the game
        if (launch == 2f)
        {
            var step = 35f * Time.deltaTime;
            mech.transform.position = Vector3.MoveTowards(mech.transform.position, waypoint2.position, step);

            if (mech.transform.position == waypoint2.position)
            {
                launch = 0f;
                loadMain();
            }
        }
    }

    public void playGame()
    {
        menuObjects.SetActive(false);
        // Safety measure to prevent the button from activating multiple times
        if (launch == 0)
        {
            launch = 1f;
        }
    }

    public void exitGame()
    {
        Application.Quit();
    }

    public void setToClipboard()
    {
        GUIUtility.systemCopyBuffer = Link;
        PlayerPrefs.SetInt("StoredReviews", 1);
        PlayerPrefs.Save();
        PlayerPrefs.Save();
    }

    // Load the main level
    public void loadMain()
    {
        SceneManager.LoadScene("Scene");
    }
}
