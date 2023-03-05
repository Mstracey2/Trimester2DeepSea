using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMovement : MonoBehaviour
{
    private Vector3 restingPos;
    public SpawnerObstacleInfo currentTrack;
    public float movementSpeed = 0;
    private bool running = false;
    private Transform target;
    public void Start()
    {
        restingPos = transform.position;
    }

    void Update()
    {
        if (running)
        {
            float step = movementSpeed * GameManager.currentManager.thisLevel.speedMultiplier * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position,step);
            if(transform.position == target.transform.position)
            {
                returnToRest();
            }
            
        }
    }

    public void returnToRest()
    {
        transform.position = restingPos;
        running = false;
        currentTrack.listOfAnimals.Remove(this);
    }


    public void obstacleActive(bool check)
    {
        running = check;
    }

    public bool checkRunning()
    {
        return running;
    }

    public void setTarget(Transform tar)
    {
        target = tar;
    }
}
