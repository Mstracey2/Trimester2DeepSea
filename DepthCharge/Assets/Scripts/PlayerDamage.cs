using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour           // script that holds the infomation and important functions for the state of the player
{  
    
    // Create a list to house the player's limb objects
    //  public List<GameObject> limbs = new List<GameObject>();
    // Minimum limb number: set in editor
    public int minNumOfLimbs;
    public bool[] limbsRemoved = new bool[4];
    public List<GameObject> limbs = new List<GameObject>();                     //limbs the player still has
    public List<GameObject> DamagedLimbs = new List<GameObject>();              // limbs the player has damaged
    [SerializeField] private List<Renderer> childrensRenders = new List<Renderer>();
    private Renderer playerRend;
    private float dullTimer = 0;                                                //timer for keeping track of how long the player stays invincible for
    private bool inDull = false;                                                //checks if player is invincible
    public PercentageBarScript percentageBarScript;




    private void Start()
    {
        foreach(GameObject thisChild in limbs)
        {
            childrensRenders.Add(thisChild.GetComponent<Renderer>());                   //gets renderer for each limb
        }
        playerRend = GetComponent<Renderer>();

    }

    public void Update()
    {
        percentageBarScript.currentInput = limbs.Count;
        
        if (limbs.Count <= minNumOfLimbs)
        {
            if (GameManager.currentManager.gameStart == true)
            {
              GameManager.currentManager.EndGame();
            }
        }

        if (inDull)                                                                 //if player has been hit and is now invincible
        {
            dullTimer -= Time.deltaTime;                                            //timer starts
            playerRend.enabled = !playerRend.enabled;                               //this will make the mesh of the object flash to visually show the player is invincible
            foreach (Renderer thisRend in childrensRenders)                         //does the same to each limb
            {
                thisRend.enabled = !thisRend.enabled;
            }

            if (dullTimer <= 0)                                                     //when time is over
            {
                dullTimer = 0;
                inDull = false;
                BreakDull();                                                        //the player is back in play

                //safety net to make sure all renderers return to enabled so nothing becomes invisible
                playerRend.enabled = true;
                foreach (Renderer thisRend in childrensRenders)
                {
                    thisRend.enabled = true;
                }
            }
        }
    }


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
        gameObject.layer = LayerMask.NameToLayer("HurtLayer");              //these layers make it so for a while, collisions are disabled
        foreach (GameObject thisChild in limbs)
        {
            thisChild.gameObject.layer = LayerMask.NameToLayer("DullZone");
        }
        dullTimer = 2;
        inDull = true;
    }

    public void BreakDull()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");                //layers return to normal and player is back in play

        foreach (GameObject thisChild in limbs)
        {
            thisChild.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit") && limbs.Count > 0)                  //if player collides with obstacle
        {
            GameObject chosenLimb = limbs[Random.Range(0, limbs.Count)];                //chooses a random limb to dislocate
            chosenLimb.GetComponent<PlayerDamageLimb>().RemoveLimb();                   //removes the limb
        }
        else if (collision.gameObject.CompareTag("Hit"))
        {
            DullPlayer();
        }
    }

    public void RemoveLimbFromList(GameObject limb)                                     //function that transfers limb objects across limb lists
    {
        limbs.Remove(limb);
        DamagedLimbs.Add(limb);
    }
}
