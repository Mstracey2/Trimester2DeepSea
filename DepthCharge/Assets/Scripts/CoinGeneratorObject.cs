using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGeneratorObject : MonoBehaviour
{
    // Start is called before the first frame update


    public Rigidbody thisBody;
    public GameObject coinObject;
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
        InvokeRepeating("SpawnCoin", 10, 3);
    }
     
    public void SpawnCoin()
    {
        Instantiate(coinObject, new Vector3 (this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);

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
}
