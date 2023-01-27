using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    // Create a list to house the player's limb objects
    public List<GameObject> limbs = new List<GameObject>();
    // Minimum limb number: set in editor
    public int minNumOfLimbs;

    private void Start()
    {
        // Populate the limb list
        foreach (GameObject limb in GameObject.FindGameObjectsWithTag("PlayerLimb"))
        {
            limbs.Add(limb);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            Debug.Log("Player Hit by Obstacle");

            // If the player still has a number of limbs over the minimum, then delete one...
            if (limbs.Count >= minNumOfLimbs)
            {
                // Currently gonna set it up as a random limb being destroyed.
                GameObject tempObj = limbs[0];
                Destroy(tempObj);
                limbs.RemoveAt(0);
                Debug.Log("Limb destroyed");
            }
            // else, the player dies
            else
            {
                Debug.Log("Player is dead lol");
            }
        }
    }
}
