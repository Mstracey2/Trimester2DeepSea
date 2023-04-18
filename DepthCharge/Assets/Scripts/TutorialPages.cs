using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialPages : MonoBehaviour
{
    public GameObject parent;
    public int currentPage;
    public GameObject[] page = new GameObject[6];
    public TextMeshProUGUI pageNumber;


    void Start()
    {
        currentPage = 0;
        page[currentPage].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        pageNumber.text = (currentPage+1).ToString() + " / " + page.Length;
    }

    public void BackButton()
    {
        if (currentPage >= 1)
        {
            page[currentPage].SetActive(false);
            currentPage--;
            page[currentPage].SetActive(true);
        }
    }

    public void ForwardButton()
    {
        if (currentPage <= 4)
        {
            page[currentPage].SetActive(false);
            currentPage++;
            page[currentPage].SetActive(true);
        }
    }

    public void CloseButton()
    {
        parent.SetActive(false);
    }

    public void openTutorial()
    {
        parent.SetActive(true);
    }
}
