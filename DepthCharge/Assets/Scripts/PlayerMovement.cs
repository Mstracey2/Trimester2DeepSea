using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;
    [SerializeField] private GameObject Inventory;
    [SerializeField] private BlipScript radarBlip;

    private void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        FindObjectOfType<AudioManager>().Play("Level Music");
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        if(x != 0)                          //if A or D is pressed, add a force in the correct direction
        {
            playerRB.AddForce(new Vector3(x, 0, 0) * 30);
        }
        if(y != 0)                         //if W or S is pressed, add a force in the correct direction
        {
            playerRB.AddForce(new Vector3(0, y, 0) * 30);
        }

        if (Input.GetKey(KeyCode.Space))    //if space is pressed, then add force in the direction the player is moving
        {
            playerRB.AddForce(new Vector3(x, y, 0) * 75);
        }
        
        // Trigger the radar
        if (Input.GetKey(KeyCode.Q))
        {
            radarBlip.active(true);
        }
    }
}
