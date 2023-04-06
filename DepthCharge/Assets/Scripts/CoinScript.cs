using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public float scale = 0;
    public bool despawning = false;
    public GameObject coinTracker;
    public CoinTracker coinsTrackerScript;

    void Start()
    {
        coinTracker = GameObject.Find("CoinsTracker");
        coinsTrackerScript = coinTracker.GetComponent<CoinTracker>();

        this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        Invoke("TimeRanOut", 2);
    }

    private void Update()
    {
        if (scale <= 2)
        {
            scale += (Time.deltaTime * 2);
            this.gameObject.transform.localScale = new Vector3(scale, scale, scale);
        }

        if (despawning)
        {
            scale -= (Time.deltaTime * 3);
            if (scale <= 0)
            {
                coinsTrackerScript.Invoke("missedCoin", 0);
                DespawnSelf();

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Invoke("PlayerHit", 0);
        }
    }

    public void PlayerHit()
    {
        coinsTrackerScript.Invoke("collectedCoin", 0);
        Debug.Log("Player hit coin");
        Invoke("DespawnSelf", 0);
    }

    public void TimeRanOut()
    {

        Debug.Log("Time ran out");
        despawning = true;

    }

    public void DespawnSelf()
    {
        Destroy(this.gameObject);
    }
}
