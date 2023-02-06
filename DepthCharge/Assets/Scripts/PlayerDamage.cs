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
    public List<GameObject> children = new List<GameObject>();

    private void Start()
    {
        //// Populate the limb list
        //foreach (GameObject limb in GameObject.FindGameObjectsWithTag("PlayerLimb"))
        //{
        //    limbs.Add(limb);
        //}

        for (int i = 0; i < transform.childCount; i++)
        {
            children.Add(transform.GetChild(i).gameObject);
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
}
