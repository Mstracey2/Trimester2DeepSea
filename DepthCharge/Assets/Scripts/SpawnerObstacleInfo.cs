using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstacleInfo : MonoBehaviour            //this small script is attached to the spawners to keep track of what animals are on the path
{
    public List<EnviormentMovement> listOfAnimals = new List<EnviormentMovement>();
    public bool CheckCurrentAnimalSpeeds(EnviormentMovement newAnimal)
    {
        foreach(EnviormentMovement thisAnimal in listOfAnimals)                     //if the animal the spawner script is trying to spawn is faster than anything already on the path, it wont spawn the new object. This is used to avoid animals clipping into eachother
        {
            if(thisAnimal.movementSpeed < newAnimal.movementSpeed)
            {
                return false;
            }
        }
        return true;
    }
}
