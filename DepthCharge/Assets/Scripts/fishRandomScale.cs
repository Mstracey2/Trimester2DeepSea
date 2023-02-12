using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fishRandomScale : MonoBehaviour
{

    public Vector3 startScale;
    public float randomisation;


    void Start()
    {
        startScale = this.gameObject.transform.localScale;
    }

    public void restartScale()
    {
        this.gameObject.transform.localScale = startScale;
    }

    public void randomiseScale()
    {
        randomisation = Random.Range(0.5f, 1.1f);
        this.gameObject.transform.localScale = new Vector3(randomisation, randomisation, randomisation);
    }
}
