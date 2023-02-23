using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject[] spawnChildren;
    [SerializeField] private GameObject[] obstacles;
    private float countdown;
    private GameObject randomObstacle;
    private GameObject randomTarget;
    public int num;
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
      countdown = Randomizer(0, 1);
    }

    // Update is called once per frame
    void Update()
    {

        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
           num = (int)Randomizer(0, spawnChildren.Length);
           countdown = Randomizer(0,1);
           randomObstacle = obstacles[(int)Randomizer(0, obstacles.Length)];
           EnviormentMovement currentObstacle = randomObstacle.GetComponent<EnviormentMovement>();
           if(currentObstacle.checkRunning() == false)
           {
                GameObject location = spawnChildren[num];
                randomObstacle.transform.position = location.transform.position;
                randomObstacle.GetComponent<fishRandomScale>().randomiseScale();
                target = location.transform.GetChild(0);
                currentObstacle.setTarget(target);
                currentObstacle.RandomizeSpeed();
                currentObstacle.obstacleActive(true);
           }
        }
    }

    public float Randomizer(float min, float max)
    {
        return Random.Range(min, max);
    }
}
