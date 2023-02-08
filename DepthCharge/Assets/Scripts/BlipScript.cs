using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlipScript : MonoBehaviour
{
    [SerializeField] private Volume processingRadar;
    [SerializeField] private VolumeProfile RadarEffect;
    [SerializeField] private VolumeProfile defaultEffect;
    [SerializeField] private PercentageBarScript percentageBarScript;

    private LineRenderer blipLine;
    [SerializeField] private GameObject blipDestination;
    private float timer = 10; 
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
        timer -= Time.deltaTime;
        percentageBarScript.currentInput = timer;
        blipLine.SetPosition(0, gameObject.transform.parent.position);
        blipLine.SetPosition(1, gameObject.transform.parent.position);

        if (timer<= 0)
        {
             blipLine.SetPosition(0, transform.position);
             blipLine.SetPosition(1, gameObject.transform.parent.position);
             transform.RotateAround(gameObject.transform.parent.position, Vector3.up, angularSpeed * Time.deltaTime);

            processingRadar.profile = RadarEffect;

            if (timer <= -10)
            {
                timer = 10;
                processingRadar.profile = defaultEffect;
            }
        }
        
       
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
