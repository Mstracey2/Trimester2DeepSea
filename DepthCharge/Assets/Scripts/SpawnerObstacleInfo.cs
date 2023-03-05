using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObstacleInfo : MonoBehaviour
{
    public List<EnviormentMovement> listOfAnimals = new List<EnviormentMovement>();
    // Start is called before the first frame update


    public bool CheckCurrentAnimalSpeeds(EnviormentMovement newAnimal)
    {
        foreach(EnviormentMovement thisAnimal in listOfAnimals)
        {
            if(thisAnimal.movementSpeed < newAnimal.movementSpeed)
            {
                return false;
            }
        }
        return true;
    }
}
