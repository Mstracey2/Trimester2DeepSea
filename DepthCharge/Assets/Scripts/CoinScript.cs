using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    #region Variables
    public float scale = 0; //Starting scale of the coin object.
    public bool despawning = false; //If it is currently despawning
    public GameObject coinTracker; //The object which tracks all coins, referenced so it can get the script form
    public CoinTracker coinsTrackerScript; //The script which is required to control all coins
    public GameObject floatingText; //The prefab of the text which appears once a coin is collected
    public AppearingText spawnedText; //The script of it
    #endregion

    void Start()
    {
        coinTracker = GameObject.Find("CoinsTracker"); //Get the object
        coinsTrackerScript = coinTracker.GetComponent<CoinTracker>(); //Get the script off of that object

        this.gameObject.transform.localScale = new Vector3(scale, scale, scale); //Start as 0 scale.
        Invoke("TimeRanOut", 2); //Run the function after two seconds.
    }

    private void Update()
    {
        if (scale <= 2) //If two seconds have passed..
        {
            scale += (Time.deltaTime * 2); //Add to scale x2 time
            this.gameObject.transform.localScale = new Vector3(scale, scale, scale); //Set the scale to move
        }

        if (despawning)
        {
            scale -= (Time.deltaTime * 3); //Remove from scale x3 time.
            if (scale <= 0)
            {
                coinsTrackerScript.Invoke("missedCoin", 0); //Tell the master function that the coin was missed
                DespawnSelf(); //Run the DespawnSelf function

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") //If the object it triggered with was the player
        {
            GameObject spawnedObj = Instantiate(floatingText, transform.position, transform.rotation); //Spawn the floating text at the same location as the coin was
            Invoke("PlayerHit", 0); //Run the function PlayerHit

            spawnedObj.GetComponent<AppearingText>().textString = "+ " + (coinsTrackerScript.streak).ToString(); //Tell the floating text what to display

        }
    }
    /// <summary>
    /// Function that runs after the player collides with.
    /// Tells the manager the player got it and then destroys itself
    /// </summary>
    public void PlayerHit()
    {
        coinsTrackerScript.Invoke("collectedCoin", 0);
        Invoke("DespawnSelf", 0);
    }

    /// <summary>
    /// Runs after the player didn't reach the coin in time.
    /// Starts despawning itself.
    /// </summary>
    public void TimeRanOut()
    {
        despawning = true;

    }

    /// <summary>
    /// Removes the current game object. 
    /// </summary>
    public void DespawnSelf()
    {
        Destroy(this.gameObject);
    }
}
