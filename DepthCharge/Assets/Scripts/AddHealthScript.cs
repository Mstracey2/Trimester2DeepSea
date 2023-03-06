using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthScript : MonoBehaviour
{
    [SerializeField] private PlayerDamage playerStatus;
    private AbilityScript abilityBubble;
    GameObject newLimb;



    void Start()
    {
        abilityBubble = GetComponent<AbilityScript>();
        abilityBubble.ability = addLimb;
    }

    public void addLimb()
    {
        if (playerStatus.DamagedLimbs.Count > 0)
        {
          newLimb = playerStatus.DamagedLimbs[Random.Range(0, playerStatus.DamagedLimbs.Count)];
        }
        
        if(newLimb != null)
        {
            playerStatus.DamagedLimbs.Remove(newLimb);
            playerStatus.limbs.Add(newLimb);
            PlayerDamageLimb currentLimb = newLimb.GetComponent<PlayerDamageLimb>();
            newLimb.GetComponent<PlayerDamageLimb>().limbStatus[0].SetActive(true);
            newLimb.GetComponent<PlayerDamageLimb>().limbStatus[1].SetActive(false);
            newLimb.transform.parent = playerStatus.gameObject.transform;
            newLimb.transform.localPosition = currentLimb.limbPosition;
            newLimb.transform.localRotation = currentLimb.limbRotation;
            newLimb.gameObject.GetComponent<Rigidbody>().isKinematic = true;
            newLimb.layer = LayerMask.NameToLayer("Limbs");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
