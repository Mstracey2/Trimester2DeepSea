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
    }

    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        
        if(x != 0)
        {
            playerRB.AddForce(new Vector3(x, 0, 0) * 30);
        }
        if(y != 0)
        {
            playerRB.AddForce(new Vector3(0, y, 0) * 30);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            playerRB.AddForce(new Vector3(x, y, 0) * 75);
        }
        
        if (Input.GetKey(KeyCode.Q))
        {
            radarBlip.active(true);
        }
    }
}
