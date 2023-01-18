using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Test"))
        {
            //    this.GetComponent<FixedJoint>().breakForce = -1f;
            //    this.GetComponent<Rigidbody>().isKinematic = false;

        }
        else
        {

        }
    }
}
