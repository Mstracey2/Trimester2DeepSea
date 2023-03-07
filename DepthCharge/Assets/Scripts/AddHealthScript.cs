using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthScript : MonoBehaviour                //script used for the health/repair powerup. When the player picks up the powerup, the player recieves a lost limb
{
    [SerializeField] private PlayerDamage playerStatus;
    private AbilityScript abilityBubble;
    GameObject newLimb;                                     // reference to the limb that will be replaced

    void Start()
    {
        abilityBubble = GetComponent<AbilityScript>();
        abilityBubble.ability = addLimb;                    // the delegate in the basic ability script is set to add limb, this is for the health powerup
    }

    public void addLimb()
    {
        newLimb = null;

        if (playerStatus.DamagedLimbs.Count > 0)            // if there are limbs damaged
        {
          newLimb = playerStatus.DamagedLimbs[Random.Range(0, playerStatus.DamagedLimbs.Count)];            //select a random limb to repair
        }
        
        if(newLimb != null)                                 //if limb was found
        {
            playerStatus.DamagedLimbs.Remove(newLimb);      //remove that limb from the damaged list
            playerStatus.limbs.Add(newLimb);                // put the lib back in active limbs
            PlayerDamageLimb currentLimb = newLimb.GetComponent<PlayerDamageLimb>();
            newLimb.GetComponent<PlayerDamageLimb>().limbStatus[0].SetActive(true);
            newLimb.GetComponent<PlayerDamageLimb>().limbStatus[1].SetActive(false);
            newLimb.transform.parent = playerStatus.gameObject.transform;               //return the limbs transform back to the original position and rotation on the player, the limb also returns to being a child of the player
            newLimb.transform.localPosition = currentLimb.limbPosition;
            newLimb.transform.localRotation = currentLimb.limbRotation;
            newLimb.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            newLimb.layer = LayerMask.NameToLayer("Limbs");                             //layer returns to limbs to that collision is active again
        }
    }
}
