using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageLimb : MonoBehaviour
{
    [SerializeField] string limbName;
    [SerializeField] PlayerDamage playerDamageScript;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            playerDamageScript.RemoveLimb(this.gameObject, limbName);
            Destroy(collision.gameObject);
        }
    }

}
