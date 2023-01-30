using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;

    private void Start()
    {
      playerRB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        if(x != 0)
        {
            playerRB.AddForce(new Vector3(x, 0, 0) * 10);
        }
        if(y != 0)
        {
            playerRB.AddForce(new Vector3(0, y, 0) * 10);
        }
    }
}
