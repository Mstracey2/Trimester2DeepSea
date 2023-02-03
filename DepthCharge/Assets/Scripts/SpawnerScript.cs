using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public Transform[] spawnChildren;
    [SerializeField] private GameObject[] obstacles;
    private float countdown;
    private GameObject randomObstacle;

    // Start is called before the first frame update
    void Start()
    {
      spawnChildren =  GetComponentsInChildren<Transform>();
      countdown = Randomizer(0, 1);
    }

    // Update is called once per frame
    void Update()
    {

        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
           countdown = Randomizer(0,1);
           randomObstacle = obstacles[(int)Randomizer(0, obstacles.Length)];
           EnviormentMovement currentObstacle = randomObstacle.GetComponent<EnviormentMovement>();
           if(currentObstacle.checkRunning() == false)
           {
                randomObstacle.transform.position = spawnChildren[(int)Randomizer(1, spawnChildren.Length)].position;

                currentObstacle.obstacleActive(true);
           }
        }
    }

    public float Randomizer(float min, float max)
    {
        return Random.Range(min, max);
    }
}
