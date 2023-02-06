using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageLimb : MonoBehaviour
{
    [SerializeField] string limbName;
    [SerializeField] PlayerDamage playerDamageScript;
    [SerializeField] GameObject[] limbStatus = new GameObject[2];

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {

            collision.gameObject.GetComponent<EnviormentMovement>().returnToRest();
            playerDamageScript.RemoveLimb(this.gameObject, limbName);
            limbStatus[0].SetActive(false);
            limbStatus[1].SetActive(true);
            Destroy(collision.gameObject);
        }
    }

}
