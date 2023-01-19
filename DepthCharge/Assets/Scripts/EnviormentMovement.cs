using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMovement : MonoBehaviour
{

    public float movementSpeed = 2;
    public bool running = true;

    void Update()
    {
        if (running)
        {
            transform.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
    }

}
