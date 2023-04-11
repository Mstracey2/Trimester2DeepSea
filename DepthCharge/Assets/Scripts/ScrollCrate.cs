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

        #region Variables
        this.transform.localPosition = new Vector3(0, 26, 0); //Set the moving items to the correct start position
        stopping = false; //If it has started to slow down
        wonTitle.text = ""; //The item which has been won
        timer = 5; //Time before the roll stops on its own
        gambled = false; //If the current role has been gambled from a previous win
        rollStoppedRunOnce = true; //Make sure it only runs once
        speed = 50000; //Speed that the roll moves at
        randomness = Random.Range(100, 300); //Randomness for when to stop 
        collectButton.SetActive(false);
        gambleButton.SetActive(false);
        wonScreen.SetActive(false);
        stopButton.SetActive(true);
        loseScreen.SetActive(false);
        gambleButtons.SetActive(false);
        normalButtons.SetActive(true);
        #endregion
    }

    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * speed; //Scroll for the player 
        timer = timer - Time.deltaTime * 100; //Count down

        if (timer < 0) //If the timer has reached 0...
        {
            stopping = true; //Start stopping 
        }
        if (stopping == true) //If stopping has started...
        {
            stopButton.SetActive(false); //Remove the stop button

            if (speed > 0) //If the roll is still moving...
            {
                speed = speed - Time.deltaTime * randomness * 10000; //Move at a random speed
                if (speed <= 0) //If speed has reached 0
                {
                    speed = 0; //Make sure its not moving
                    if (rollStoppedRunOnce == true) //Make sure it only runs once...
                    { 
                        RollStopped();
                    }
                }
            }
        }
    }
    /// <summary>
    /// Function that runs once the roll has finished
    /// </summary>
    public void RollStopped()
    {
        rollStoppedRunOnce = false;
        closestButton.TryGetComponent<InventoryButtons>(out InventoryButtons chosenButton); //Get the script for the closest button

        if (chosenButton != null) //If its succsessful...
        {
            collectButton.SetActive(true); //Turn on the collect button

            if (gambled == false) //If it hasn't already been gambled...
            {
                gambleButton.SetActive(true); //Allow the player to gamble again...
            }
        }
        else
        {
            loseScreen.SetActive(true); //Else, its not a successful run, the player has landed on a loss and turn on the loss screen
        }
    }

    public void Stop()
    {
        stopping = true;
    }
    /// <summary>
    /// Button ran when the player presses Collect on a winning button
    /// </summary>
    public void Collect()
    {
        gambleButton.SetActive(false); //Remove the gamble button
        wonScreen.SetActive(true); //Enable the won screen
        itemNumber = closestButton.GetComponent<InventoryButtons>().itemNumber; //Get the number of the won item
        wonButton.GetComponent<InventoryButtons>().itemNumber = itemNumber; 
        wonButton.GetComponent<InventoryButtons>().Invoke("SetVariables", 0);
        wonTitle.text = "You Won: " + inventoryScript.cosmeticItemTitle[itemNumber]; //Set the text to the won title
        inventoryScript.unlockedBool[itemNumber] = true; //Give the player that cosmetic
        wonType.text = inventoryScript.cosmeticRarity[itemNumber]; //Display the rarity of that item
        statistics.boxesOpened++; //Add to how many times the player has opened a crate
        statistics.saveStats();
    }

    public void Claim()
    {
        crateObject.SetActive(false);
    }

    public void Enable()
    {
        inventoryScript.EnableObject(itemNumber);
    }

    /// <summary>
    /// Function runs when the player choses to gamble their winnings. Resets the scroll with 50% win and 50% lose buttons
    /// </summary>
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
