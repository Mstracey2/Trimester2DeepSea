using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMovement : MonoBehaviour
{
    [SerializeField] private GameObject destroyOrb;
    public float movementSpeed = 2;
    public bool running = true;

    void Update()
    {
        if (running)
        {
            transform.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject == destroyOrb)
        {
            Destroy(gameObject);
        }
    }

}
