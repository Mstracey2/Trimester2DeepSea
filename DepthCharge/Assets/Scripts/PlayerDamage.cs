using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{  
    
    public GameManager gameManager;
    // Create a list to house the player's limb objects
  //  public List<GameObject> limbs = new List<GameObject>();
    // Minimum limb number: set in editor
    public int minNumOfLimbs;
    public bool[] limbsRemoved = new bool[4];
    public int remainingLimbs;
    [SerializeField] private List<GameObject> children = new List<GameObject>();
    [SerializeField] private List<Renderer> childrensRenders = new List<Renderer>();
    private Renderer playerRend;
    private float dullTimer = 0;
    private bool inDull = false;
    public PercentageBarScript percentageBarScript;




    private void Start()
    {
        remainingLimbs = 4;
        foreach(GameObject thisChild in children)
        {
            childrensRenders.Add(thisChild.GetComponent<Renderer>());
        }
        playerRend = GetComponent<Renderer>();
        //// Populate the limb list
        //foreach (GameObject limb in GameObject.FindGameObjectsWithTag("PlayerLimb"))
        //{
        //    limbs.Add(limb);
        //}

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    children.Add(transform.GetChild(i).gameObject);
        //}
    }

    public void Update()
    {
        percentageBarScript.currentInput = remainingLimbs;

        if (remainingLimbs <= minNumOfLimbs)
        {
            if (gameManager.gameStart == true)
            {
                gameManager.EndGame();
            }
        }

        if (inDull)
        {
            dullTimer -= Time.deltaTime;
            playerRend.enabled = !playerRend.enabled;
            foreach (Renderer thisRend in childrensRenders)
            {
                thisRend.enabled = !thisRend.enabled;
            }

            if (dullTimer <= 0)
            {
                dullTimer = 0;
                inDull = false;
                BreakDull();
                playerRend.enabled = true;
                foreach (Renderer thisRend in childrensRenders)
                {
                    thisRend.enabled = true;
                }
            }
        }
    }
    //public void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Hit"))
    //    {
    //        Debug.Log("Player Hit by Obstacle");

    //        // If the player still has a number of limbs over the minimum, then delete one...
    //        if (limbs.Count >= minNumOfLimbs)
    //        {
    //            // Currently gonna set it up as a random limb being destroyed.
    //            GameObject tempObj = limbs[0];
    //            Destroy(tempObj);
    //            limbs.RemoveAt(0);
    //            Debug.Log("Limb destroyed");
    //        }
    //        // else, the player dies
    //        else
    //        {
    //            Debug.Log("Player is dead lol");
    //        }
    //    }
    //}

    public void RemoveLimb(string limbName)
    {
        
        Debug.Log("Removed Limb" + limbName);

        switch (limbName)
        {
            case "LeftLeg":
                limbsRemoved[0] = true;
                break;

            case "RightLeg":
                limbsRemoved[1] = true;
                break;

            case "LeftArm":
                limbsRemoved[2] = true;
                break;

            case "RightArm":
                limbsRemoved[3] = true;
                break;
        }
    }

    public void DullPlayer()
    {
        gameObject.layer = LayerMask.NameToLayer("HurtLayer");
        foreach (GameObject thisChild in children)
        {
            thisChild.gameObject.layer = LayerMask.NameToLayer("DullZone");
        }
        dullTimer = 2;
        inDull = true;
    }

    public void BreakDull()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");

        foreach (GameObject thisChild in children)
        {
            thisChild.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit") && children.Count > 0)
        {
            GameObject chosenLimb = children[Random.Range(0, children.Count)];
            chosenLimb.GetComponent<PlayerDamageLimb>().RemoveLimb();
        }
        else if (collision.gameObject.CompareTag("Hit"))
        {
            DullPlayer();
        }
    }

    public void RemoveLimbFromList(GameObject limb)
    {
        children.Remove(limb);
    }
}
