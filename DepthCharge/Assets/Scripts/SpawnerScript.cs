using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour                  //This script is in charge of spawning obstacles and abilities on tracks
{
    public GameObject[] spawnChildren;                      //spawn locations with destinations
    [SerializeField] private GameObject[] fish;             
    [SerializeField] private GameObject[] mammels;
    [SerializeField] private GameObject[] Abilities;
    private float randomNumberChance;
    private GameObject randomObstacle;
    public int num;
    public Transform target;
    GameObject location;

    void Update()
    {
        SpawnObject(fish,GameManager.currentManager.GetChance(1));              //on each frame, the script rolls a random number to attempt to spawn each type of object, this also gets the random number range, which changes how likely the object is to spawn
        SpawnObject(mammels,GameManager.currentManager.GetChance(2));
        SpawnObject(Abilities,GameManager.currentManager.GetChance(3));
    }

    public float Randomizer(int min, int max)
    {
        return Random.Range(min, max);
    }

    public void SpawnObject(GameObject[] thisObjectList, int randomMultiplier)
    {
        randomNumberChance = Random.Range(1, randomMultiplier);                 //range from get chance
        if (randomNumberChance == 2)                                            //if the number is two, the object is spawned
        {
            randomObstacle = thisObjectList[(int)Randomizer(0, thisObjectList.Length)];                 //gets a random object from the list
            EnviormentMovement currentObstacle = randomObstacle.GetComponent<EnviormentMovement>();     
            if (currentObstacle.checkRunning() == false)                                                //object will not spawn if the object chosen is already in play
            {
                currentObstacle.setTarget(GetDestination());                                            //gets the target destination

                SpawnerObstacleInfo currentTrack = location.gameObject.GetComponent<SpawnerObstacleInfo>();     

                if (currentTrack.CheckCurrentAnimalSpeeds(currentObstacle) == true)                    //object will not spawn if the track chosen has a slower object than the one trying to be spawned
                {
                    currentTrack.listOfAnimals.Add(currentObstacle);                                    //adds the object to the list of obstacles on the track
                    currentObstacle.transform.position = location.transform.position;                   //obstacle moves to spawner
                    currentObstacle.currentTrack = currentTrack;
                    currentObstacle.obstacleActive(true);                                               //obstacle is now in play and running
                }

            }
        }
    }

    public Transform GetDestination()
    {
        num = (int)Randomizer(0, spawnChildren.Length);                 
        location = spawnChildren[num];                                  //gets a random spawner
        return location.transform.GetChild(0); ;                        //returns destination alligned with spawner
    }

}
