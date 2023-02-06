using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageLimb : MonoBehaviour
{
    [SerializeField] string limbName;
    [SerializeField] PlayerDamage playerDamageScript;
    [SerializeField] GameObject[] limbStatus = new GameObject[2];
    private Rigidbody limbRB;

    private void Start()
    {
        limbRB = GetComponent<Rigidbody>();
    }

    public void RemoveLimb()
    {
        this.gameObject.transform.parent = null;
        limbRB.isKinematic = false;
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

        this.gameObject.layer = LayerMask.NameToLayer("Dull");
        playerDamageScript.RemoveLimb(limbName);
        limbStatus[0].SetActive(false);
        limbStatus[1].SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))
        {
            RemoveLimb();
        }
    }
}
