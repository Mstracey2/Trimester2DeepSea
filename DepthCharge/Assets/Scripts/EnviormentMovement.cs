using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMovement : MonoBehaviour
{
    private Vector3 restingPos;
    [SerializeField] private List<GameObject> revertOnCollisionObjects = new List<GameObject>();
    private float movementSpeed = 0;
    private bool running = false;

    private void Start()
    {
        restingPos = transform.position;
    }

    void Update()
    {
        if (running)
        {
            movementSpeed = Random.Range(5, 20);
            transform.position += Vector3.back * Time.deltaTime * movementSpeed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        transform.position = restingPos;
        running = false;
    }


    public void obstacleActive(bool check)
    {
        running = check;
    }

    public bool checkRunning()
    {
        return running;
    }

}
