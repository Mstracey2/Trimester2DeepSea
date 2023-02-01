using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollCrate : MonoBehaviour
{
    [SerializeField] float speed;
    public bool stopping;
    private float randomness;

    private void Start()
    {
        randomness = Random.Range(100, 300);
    }

    void Update()
    {
        this.transform.position += Vector3.right * Time.deltaTime * speed;

        if (stopping == true)
        {
            if (speed > 0)
            {
                speed = speed - Time.deltaTime * randomness;
                if (speed < 0)
                {
                    speed = 0;
                }
            }
        }
    }
}
