using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMove : MonoBehaviour
{

    public float movementSpeed;
    public bool running = true;


    void Start()
    {
        
    }

    void Update()
    {
        if (running)
        {
            transform.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
    }


}
