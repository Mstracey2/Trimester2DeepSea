using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] spawnChildren;
    [SerializeField] private GameManager manager;
    [SerializeField] private GameObject[] fish;
    [SerializeField] private GameObject[] mammels;
    private float countdown;
    private GameObject randomObstacle;
    private GameObject randomTarget;
    public int num;
    public Transform target;
    GameObject location;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        SpawnFish(fish, manager.GetFishChance(1));
        SpawnFish(mammels, manager.GetFishChance(2));
          
    }

    public float Randomizer(int min, int max)
    {
        return Random.Range(min, max);
    }

    public void SpawnFish(GameObject[] thisFishList, int randomMultiplier)
    {
        countdown = Random.Range(1, randomMultiplier);
        if (countdown == 2)
        {
            randomObstacle = thisFishList[(int)Randomizer(0, thisFishList.Length)];
            EnviormentMovement currentObstacle = randomObstacle.GetComponent<EnviormentMovement>();
            if (currentObstacle.checkRunning() == false)
            {
                randomObstacle.GetComponent<fishRandomScale>().randomiseScale();
                currentObstacle.setTarget(GetDestination());
                currentObstacle.obstacleActive(true);
            }
        }
    }

    public Transform GetDestination()
    {
        num = (int)Randomizer(0, spawnChildren.Length);
        location = spawnChildren[num];
        randomObstacle.transform.position = location.transform.position;
        return location.transform.GetChild(0); ;
    }
}
