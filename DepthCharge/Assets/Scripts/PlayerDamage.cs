using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{  
    
    // Create a list to house the player's limb objects
    //  public List<GameObject> limbs = new List<GameObject>();
    // Minimum limb number: set in editor
    public int minNumOfLimbs;
    public bool[] limbsRemoved = new bool[4];
    public List<GameObject> limbs = new List<GameObject>();
    public List<GameObject> DamagedLimbs = new List<GameObject>();
    [SerializeField] private List<Renderer> childrensRenders = new List<Renderer>();
    private Renderer playerRend;
    private float dullTimer = 0;
    private bool inDull = false;
    public PercentageBarScript percentageBarScript;




    private void Start()
    {

        foreach(GameObject thisChild in limbs)
        {
            childrensRenders.Add(thisChild.GetComponent<Renderer>());
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
        foreach (GameObject thisChild in limbs)
        {
            thisChild.gameObject.layer = LayerMask.NameToLayer("DullZone");
        }
        dullTimer = 2;
        inDull = true;
    }

    public void BreakDull()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Player");

        foreach (GameObject thisChild in limbs)
        {
            thisChild.gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit") && limbs.Count > 0)
        {
            GameObject chosenLimb = limbs[Random.Range(0, limbs.Count)];
            chosenLimb.GetComponent<PlayerDamageLimb>().RemoveLimb();
        }
        else if (collision.gameObject.CompareTag("Hit"))
        {
            DullPlayer();
        }
    }

    public void RemoveLimbFromList(GameObject limb)
    {
        limbs.Remove(limb);
        DamagedLimbs.Add(limb);
    }
}
