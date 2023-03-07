using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageLimb : MonoBehaviour                       //script for the player limbs, used for the detatching of limb
{
    [SerializeField] string limbName;
    private PlayerDamage playerDamageScript;
    [SerializeField]public GameObject[] limbStatus = new GameObject[2];
    private Rigidbody limbRB;
    //values that hold original pos/rot of limbs, used when returning a limb to the player (picks up health)
    public Vector3 limbPosition;
    public Quaternion limbRotation;


    private void Start()
    {
        limbRB = GetComponent<Rigidbody>();
        limbPosition = transform.localPosition;
        limbRotation = transform.localRotation;
        playerDamageScript = gameObject.transform.parent.GetComponent<PlayerDamage>();
    }

    public void RemoveLimb()
    {
        playerDamageScript.RemoveLimbFromList(this.gameObject);
        this.gameObject.transform.parent = null;                    //detaches the limb from the mech
        limbRB.isKinematic = false;                                 //the limb is now affected by forces
        // this decides which way the limbs are ejected from the player, if the player loses a right limb, force is applied so it goes offscreen to the right. same process for the left
        if (limbName.Contains("Right"))
        {
            limbRB.AddForce(Vector3.right * 100);
            limbRB.AddTorque(Vector3.right * 100);
        }
        else
        {
            limbRB.AddForce(Vector3.left * 100);
            limbRB.AddTorque(Vector3.left * 100);
        }

        this.gameObject.layer = LayerMask.NameToLayer("DullZone");      //limb enters a layer which avoids collision
        playerDamageScript.RemoveLimb(limbName);                        
        limbStatus[0].SetActive(false);
        limbStatus[1].SetActive(true);
        playerDamageScript.DullPlayer();                                //player is temporaraly invincible
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))                     //if the limb itself is hit, then it removes itself.
        {
            RemoveLimb();
        }
    }
}
