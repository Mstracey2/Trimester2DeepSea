using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneratorObject : MonoBehaviour
{
    public Rigidbody thisBody; //Reference to selfs rigid body
    public GameObject coinObject; //Reference to the prefab of the coin object which is spawned
    private float addVelocity = 300f; //Force that is added
    void Start()
    {
        Invoke("StartMovement", 0);
    }

    /// <summary>
    /// Pick a random direction and move there
    /// </summary>
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

        InvokeRepeating("SpawnCoin", 10, 3); //Spawn a new coin, after 10 seconds, then every 3 seconds.
    }
    /// <summary>
    /// Spawn a coin at the current location as this object, repeating every 3 seconds. Once spawned the parent object will move randomly again.
    /// </summary>
    public void SpawnCoin()
    {
        Instantiate(coinObject, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
        //Spawn the object at the same location.


        float random = Random.Range(0, 2); //Pick which random direction to move

        if (random < 1)
        {
            thisBody.AddForce(new Vector3(addVelocity, addVelocity, 0));
        }
        else
        {
            thisBody.AddForce(new Vector3(-addVelocity, -addVelocity, 0));
        }
    }
}
