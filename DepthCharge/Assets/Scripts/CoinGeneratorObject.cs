using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneratorObject : MonoBehaviour
{
    // Start is called before the first frame update


    public Rigidbody thisBody;
    private float addVelocity = 300f;
    void Start()
    {

        Invoke("StartMovement", 0);
    }

    public void StartMovement()
    {
        float random = Random.Range(0, 2);
        if (random < 1)
        {
            thisBody.AddForce(new Vector3(addVelocity, addVelocity, 0));
        }
        else
        {
            thisBody.AddForce(new Vector3(-addVelocity, -addVelocity, 0));
        }
    }

    public void Update()
    {
        if (thisBody.velocity.x == 0)
        {
            thisBody.velocity = new Vector3(Random.Range(1, 3), thisBody.velocity.y,0);
        }
        else if(thisBody.velocity.y == 0)
        {
            thisBody.velocity = new Vector3(thisBody.velocity.x, Random.Range(1, 3),0);
        }
    } 

}
