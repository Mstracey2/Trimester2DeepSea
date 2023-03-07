using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlipScript : MonoBehaviour                                         // the Main script for the radar mechanic.
{
    //diffierent post processing values to give off a radar effect
    [SerializeField] private Volume processingRadar;
    [SerializeField] private VolumeProfile RadarEffect;
    [SerializeField] private VolumeProfile defaultEffect;

    [SerializeField] private PercentageBarScript percentageBarScript;

    //values used to make the radar line bounce back and forth
    [SerializeField] private Transform startLocation;
    [SerializeField] private GameObject blipDestination;

    Color colour;
    private LineRenderer blipLine;
    private float timer = 10; 
    [SerializeField] private float angularSpeed = -500;     //degrees per second
    [SerializeField] private float newDestination = 180;    //sets a new destination for the collision object that the radar line needs to hit in order to change direction

    public bool activated;

    private string radarFog = "78C879";

    // Start is called before the first frame update
    void Start()
    {
        blipLine = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer < 10 && !activated)                        // timer for radar cooldown, code below in activated if statement wont run until the timer returns to 10 seconds
        {
            timer += Time.deltaTime;
        }
        
        percentageBarScript.currentInput = timer;
        percentageBarScript.changeColour("green");

        //the starting position for the line renderer is at the base of the mech model
        blipLine.SetPosition(0, startLocation.transform.position);
        blipLine.SetPosition(1, startLocation.transform.position);

        if (activated)
        {
            timer -= Time.deltaTime;                        //start timer
            // line render goes from the player, towards this game object, which is an object that bounces back and forth between two points
            blipLine.SetPosition(0, transform.position);
            blipLine.SetPosition(1,startLocation.transform.position);
             transform.RotateAround(startLocation.transform.position, Vector3.up, angularSpeed * Time.deltaTime);               // does a arching motion       
             percentageBarScript.currentInput = Mathf.Abs(timer);
            percentageBarScript.changeColour("red");
             
             ChangeFogColour(radarFog);                    // changes the global fog colour to reveal obtacles   
             processingRadar.profile = RadarEffect;        // post processing effect changes to radar effect (TV Static and green colour)
            
            // Radar end condition
            if (timer <= 0)
            {
                processingRadar.profile = defaultEffect;    //PP effect returns to normal
                ChangeFogColour(GameManager.currentManager.thisLevel.cameraBackgroundColour);           //fog colour returns to the level fog colour to hide obstacles again
                activated = false;
            }
        }
        
       
    }

    private void ChangeFogColour(string hexCol)
    {
        
       if(ColorUtility.TryParseHtmlString("#" + hexCol, out colour))                                    // if hex colour code is accurate, it returns the colour
        {
            RenderSettings.fogColor = colour;                                                           // fog colour changes
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == blipDestination)             //if the bouncing object(object this script is attached to) hits the destination
        {
            angularSpeed = angularSpeed * -1;               //the orb goes back the other way
            newDestination = newDestination * -1;           // destination rotates 180 to the other side of the player
            other.transform.RotateAround(gameObject.transform.parent.position, Vector3.up, newDestination);
        }
    }

    public void active(bool act)
    {
        if(timer >= 10)                                     // if timer says radar is ready. the radar is activated
        {
            FindObjectOfType<AudioManager>().Play("Radar Blip");
            activated = act;
        }
    }
}
