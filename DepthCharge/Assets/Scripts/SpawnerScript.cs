using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] spawnChildren;
    [SerializeField] private GameManager manager;
    [SerializeField] private GameObject[] fish;
    [SerializeField] private GameObject[] mammels;
    [SerializeField] private GameObject[] Abilities;
    private float randomNumberChance;
    private GameObject randomObstacle;
    private GameObject randomTarget;
    public int num;
    public Transform target;
    GameObject location;

    public float AbilitiesSpawner = 0;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        AbilitiesSpawner += Time.deltaTime;
        SpawnObject(fish, manager.GetChance(1));
        SpawnObject(mammels, manager.GetChance(2));
        if(AbilitiesSpawner >= 0.01)
        {
          SpawnObject(Abilities, manager.GetChance(3));
        }  
    }

    public float Randomizer(int min, int max)
    {
        return Random.Range(min, max);
    }

    public void SpawnObject(GameObject[] thisObjectList, int randomMultiplier)
    {
        randomNumberChance = Random.Range(1, randomMultiplier);
        if (randomNumberChance == 2)
        {
            randomObstacle = thisObjectList[(int)Randomizer(0, thisObjectList.Length)];
            EnviormentMovement currentObstacle = randomObstacle.GetComponent<EnviormentMovement>();
            if (currentObstacle.checkRunning() == false)
            {
                currentObstacle.setTarget(GetDestination());

                SpawnerObstacleInfo currentTrack = location.gameObject.GetComponent<SpawnerObstacleInfo>();

                if (currentTrack.CheckCurrentAnimalSpeeds(currentObstacle) == true)
                {
                    currentTrack.listOfAnimals.Add(currentObstacle);
                    currentObstacle.transform.position = location.transform.position;
                    currentObstacle.currentTrack = currentTrack;
                    currentObstacle.obstacleActive(true);
                }

            }
        }
    }

    public Transform GetDestination()
    {
        num = (int)Randomizer(0, spawnChildren.Length);
        location = spawnChildren[num];
        return location.transform.GetChild(0); ;
    }

}
