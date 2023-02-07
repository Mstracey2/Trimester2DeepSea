using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody playerRB;
    [SerializeField] private GameObject Inventory;

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
            playerRB.AddForce(new Vector3(x, 0, 0) * 15);
        }
        if(y != 0)
        {
            playerRB.AddForce(new Vector3(0, y, 0) * 15);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            playerRB.AddForce(new Vector3(x, y, 0) * 50);
        }


    //    if(Input.GetKeyDown(Keycode.M))
      //  {
        //    Inventory.SetActive(true);
        //}

        //if(Input.GetKeyDown("N"))
        //{
          //  Inventory.SetActive(false);
        //}
    }
}
