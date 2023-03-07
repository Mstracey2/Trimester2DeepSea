using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMovement : MonoBehaviour
{
    // position the object returns to when not being used
    private Vector3 restingPos;
    // holds the track the object is currently on
    public SpawnerObstacleInfo currentTrack;
    //speed of the obstacle
    public float movementSpeed = 0;
    //used to check if the object is in play
    private bool running = false;
    // location the object is trying to get to
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
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position,step);       //moves towards its target at its speed * by the multiplier
            if(transform.position == target.transform.position)                                                 //when the object reaches the target, it returns to its resting spot out of play
            {
                returnToRest();
            }
            
        }
    }

    public void returnToRest()              //goes out of play
    {
        transform.position = restingPos;
        running = false;
        currentTrack.listOfAnimals.Remove(this);        //removes itself from the list of animals that is on the track it was on originally 
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
