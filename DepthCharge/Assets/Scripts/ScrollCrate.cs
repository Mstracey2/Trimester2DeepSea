using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollCrate : MonoBehaviour
{
    [SerializeField] float speed;
    public InventoryScript inventoryScript;
    public bool stopping;
    private float randomness;
    private float timer = 5;
    private bool rollStoppedRunOnce = true;
    public int itemNumber;
    private bool gambled = false;
    [SerializeField] private GameObject crate;
    [SerializeField] private GameObject normalButtons;
    [SerializeField] private GameObject gambleButtons;

    [SerializeField] public GameObject closestButton;
    [SerializeField] private GameObject stopButton;
    [SerializeField] private GameObject collectButton;
    [SerializeField] private GameObject gambleButton;

    [SerializeField] private GameObject wonScreen;
    [SerializeField] private GameObject wonButton;
    [SerializeField] private TextMeshProUGUI wonTitle;
    [SerializeField] private TextMeshProUGUI wonType;

      [SerializeField] private GameObject loseScreen;

    private void Start()
    {
        randomness = Random.Range(100, 300);
        collectButton.SetActive(false);
        gambleButton.SetActive(false);
    }

    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * speed;
        timer = timer - Time.deltaTime;
        if (timer < 0)
        {
            stopping = true;
        }
        if (stopping == true)
        {
            stopButton.SetActive(false);

            if (speed > 0)
            {
                speed = speed - Time.deltaTime * randomness;
                if (speed <= 0)
                {
                    speed = 0;
                    if (rollStoppedRunOnce == true)
                    {
                        RollStopped();
                    }
                }
            }
        }
    }

    public void RollStopped()
    {
        rollStoppedRunOnce = false;


        if (closestButton.GetComponent<InventoryButtons>() != null)
        {
            collectButton.SetActive(true);
            if (gambled == false)
            {
                gambleButton.SetActive(true);
            }
        }
        else
        {
            loseScreen.SetActive(true);
        }



    }

    public void Stop()
    {
        stopping = true;
    }

    public void Collect()
    {
        gambleButton.SetActive(false);
        wonScreen.SetActive(true);
        itemNumber = closestButton.GetComponent<InventoryButtons>().itemNumber;
        wonButton.GetComponent<InventoryButtons>().itemNumber = itemNumber;
        wonButton.GetComponent<InventoryButtons>().Invoke("SetVariables", 0);
        wonTitle.text = "You Won: " + inventoryScript.cosmeticItemTitle[itemNumber];
        inventoryScript.unlockedBool[itemNumber] = true;
        wonType.text = inventoryScript.cosmeticRarity[itemNumber];
    }

    public void Claim()
    {
        crate.gameObject.SetActive(false);
    }

    public void Enable()
    {
        inventoryScript.DespawnOfType(inventoryScript.cosmeticItemType[itemNumber]);
        inventoryScript.cosmeticItemObject[itemNumber].SetActive(true);
    }

    public void Gamble()
    {
        gambled = true;
        timer = 5;
        speed = 500;
        gambleButtons.SetActive(true);
        normalButtons.SetActive(false);
        rollStoppedRunOnce = true;
        stopping = false;
        gambleButton.SetActive(false);
        collectButton.SetActive(false);
        stopButton.SetActive(true);


    }


}
