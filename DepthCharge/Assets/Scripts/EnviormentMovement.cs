using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviormentMovement : MonoBehaviour
{
    private Vector3 restingPos;
    [SerializeField] private List<GameObject> revertOnCollisionObjects = new List<GameObject>();
    [SerializeField] private GameObject playerWall;
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
            float step = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position,step);
            if(transform.position == target.transform.position)
            {
                returnToRest();
              //  this.gameObject.GetComponent<fishRandomScale>().restartScale();
            }
            
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("ZoneWall"))
        {
           
        }
    }
    public void returnToRest()
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

    public void setTarget(Transform tar)
    {
        target = tar;
    }
}
