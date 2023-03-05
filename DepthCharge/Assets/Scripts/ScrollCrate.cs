using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollCrate : MonoBehaviour
{
    [SerializeField] private GameObject crateObject;
    [SerializeField] private Statistics statistics;

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

    public void StartRoll()
    {

        this.transform.localPosition = new Vector3(0, 26, 0);
        stopping = false;
        wonTitle.text = "";
        timer = 5;
        gambled = false;
        rollStoppedRunOnce = true;
        speed = 50000;
        randomness = Random.Range(100, 300);
        collectButton.SetActive(false);
        gambleButton.SetActive(false);
        wonScreen.SetActive(false);
        stopButton.SetActive(true);
     //   gameManager.PauseGame();
    }

    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * speed;
        timer = timer - Time.deltaTime * 100;
        if (timer < 0)
        {
            stopping = true;
        }
        if (stopping == true)
        {
            stopButton.SetActive(false);

            if (speed > 0)
            {
                speed = speed - Time.deltaTime * randomness * 10000;
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
        closestButton.TryGetComponent<InventoryButtons>(out InventoryButtons chosenButton);

        if (chosenButton != null)
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
        statistics.boxesOpened++;
    }

    public void Claim()
    {
        crateObject.SetActive(false);
    }

    public void Enable()
    {
        //if (inventoryScript.cosmeticItemType[itemNumber] != 2) { 
        //inventoryScript.DespawnOfType(inventoryScript.cosmeticItemType[itemNumber]);
        //inventoryScript.cosmeticItemObject[itemNumber].SetActive(true);

            inventoryScript.EnableObject(itemNumber);
    } 


    public void Gamble()
    {
        gambled = true;
        timer = 5;
        speed = 50000;
        gambleButtons.SetActive(true);
        normalButtons.SetActive(false);
        rollStoppedRunOnce = true;
        stopping = false;
        gambleButton.SetActive(false);
        collectButton.SetActive(false);
        stopButton.SetActive(true);
    }
}
