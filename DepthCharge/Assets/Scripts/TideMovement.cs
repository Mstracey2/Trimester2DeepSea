using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TideMovement : MonoBehaviour
{
    //adjust this to change speed
    float speed = 0.4f;
    //adjust this to change how high it goes up/down on the y axis from its starting position
    float height = 0.5f;
    float startHeight = -1.9f;

    void Update()
    {
        //calculate what the new Y position will be
        float newY = Mathf.Sin(Time.time * speed) * height + startHeight;
        //set the object's Y to the new calculated Y
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
