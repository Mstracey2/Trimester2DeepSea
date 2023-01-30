using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlipScript : MonoBehaviour
{
    private LineRenderer blipLine;
    [SerializeField] private GameObject blipDestination;

    [SerializeField] private float angularSpeed = -500; //degrees per second
    [SerializeField] private float newDestination = 180;

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

        transform.RotateAround(gameObject.transform.parent.position, Vector3.up, angularSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == blipDestination)
        {
            angularSpeed = angularSpeed * -1;
            newDestination = newDestination * -1;
            other.transform.RotateAround(gameObject.transform.parent.position, Vector3.up, newDestination);
        }
    }
}
