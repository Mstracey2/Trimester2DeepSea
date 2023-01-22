using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipScript : MonoBehaviour
{
    private LineRenderer blipLine;
    // Start is called before the first frame update
    void Start()
    {
        blipLine = GetComponent<LineRenderer>();   
    }

    // Update is called once per frame
    void Update()
    {
        blipLine.SetPosition(0, transform.position);
        blipLine.SetPosition(1, gameObject.transform.parent.position);

        transform.RotateAround(gameObject.transform.parent.position, Vector3.up, 1000 * Time.deltaTime);
    }
}
