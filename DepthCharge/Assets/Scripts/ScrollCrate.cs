using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScrollCrate : MonoBehaviour
{
    [SerializeField] float speed;
    public bool stopping;
    private float randomness;
    private float timer = 5;
    private bool rollStoppedRunOnce = true;
    public List<GameObject> buttons = new List<GameObject>();
    public GameObject[] buttonsObject = new GameObject[100];

    [SerializeField] private GameObject ticker;
    public GameObject closestButton;
    [SerializeField] private GameObject stopButton;
    [SerializeField] private GameObject collectButton;
    [SerializeField] private GameObject gambleButton;

    [SerializeField] private GameObject wonScreen;
  //  [SerializeField] private GameObject loseScreen;

    private void Start()
    {
        randomness = Random.Range(100, 300);
        collectButton.SetActive(false);
        gambleButton.SetActive(false);
    }

    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * speed;
        timer = timer - Time.deltaTime;
        if (timer < 0)
        {
            stopping = true;

        }
        if (stopping == true)
        {
            stopButton.SetActive(false);

            if (speed > 0)
            {
                speed = speed - Time.deltaTime * randomness;
                if (speed <= 0)
                {
                    speed = 0;
                    if (rollStoppedRunOnce == true)
                    {
                        RollStopped();
                    }
                }
            }
        }
    }

    public void RollStopped()
    {
        rollStoppedRunOnce = false;
        collectButton.SetActive(true);
        gambleButton.SetActive(true);
        //closestButton = buttonsObject.OrderBy(go => (transform.position - go.transform.position).sqrMagnitude).First().transform;

    }

    //Transform GetClosestButton(List<Transform> buttons, Transform ticker)
    //{
    //    Transform bestTarget = null;
    //    float closestDistanceSqr = Mathf.Infinity;
    //    Vector3 currentPosition = ticker.position;
    //    foreach (Transform potentialTarget in buttons)
    //    {
    //        Vector3 directionToTarget = potentialTarget.position - currentPosition;
    //        float dSqrToTarget = directionToTarget.sqrMagnitude;
    //        if (dSqrToTarget < closestDistanceSqr)
    //        {
    //            closestDistanceSqr = dSqrToTarget;
    //            bestTarget = potentialTarget;
    //        }
    //    }
    //    return bestTarget;
    //}

    public void Stop()
    {
        stopping = true;
    }

    public void Collect()
    {

    }

    public void Gamble()
    {

    }


}
