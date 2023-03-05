using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BlipScript : MonoBehaviour
{
    [SerializeField] private GameManager manager;
    [SerializeField] private Volume processingRadar;
    [SerializeField] private VolumeProfile RadarEffect;
    [SerializeField] private VolumeProfile defaultEffect;
    [SerializeField] private PercentageBarScript percentageBarScript;
    [SerializeField] private Transform startLocation;
    [SerializeField] private GameObject radarWall;
    [SerializeField] private Transform radarWallDestination;
    [SerializeField] private Vector3 radarWallOriginalPos;
    Color colour;
    private LineRenderer blipLine;
    [SerializeField] private GameObject blipDestination;
    private float timer = 10; 
    [SerializeField] private float angularSpeed = -500; //degrees per second
    [SerializeField] private float newDestination = 180;

    public bool activated;

    private string radarFog = "78C879";
    private string naturalFog;

    // Start is called before the first frame update
    void Start()
    {
        blipLine = GetComponent<LineRenderer>();
        radarWallOriginalPos = radarWall.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
     
        if(timer < 10 && !activated)
        {
            timer += Time.deltaTime;
        }
        
        percentageBarScript.currentInput = timer;
        percentageBarScript.changeColour("green");
        blipLine.SetPosition(0, startLocation.transform.position);
        blipLine.SetPosition(1, startLocation.transform.position);
        if (activated)
        {
            timer -= Time.deltaTime;
            blipLine.SetPosition(0, transform.position);
             blipLine.SetPosition(1,startLocation.transform.position);
             transform.RotateAround(startLocation.transform.position, Vector3.up, angularSpeed * Time.deltaTime);         
             percentageBarScript.currentInput = Mathf.Abs(timer);
            percentageBarScript.changeColour("red");

            MoveWall();
             
             ChangeFogColour(radarFog);              
             processingRadar.profile = RadarEffect;
            
            // Radar end condition
            if (timer <= 0)
            {
                radarWall.transform.position = radarWallOriginalPos;
                processingRadar.profile = defaultEffect;
                ChangeFogColour(manager.thisLevel.cameraBackgroundColour);
                activated = false;
            }
        }
        
       
    }

    private void ChangeFogColour(string hexCol)
    {
        
       if(ColorUtility.TryParseHtmlString("#" + hexCol, out colour))
        {
            RenderSettings.fogColor = colour;
        }
    }

    private void MoveWall()
    {
        radarWall.transform.position = new Vector3(radarWall.transform.position.x, radarWall.transform.position.y, radarWall.transform.position.z + 100 * Time.deltaTime);
        if (radarWall.transform.position.z >= radarWallDestination.transform.position.z)
        {
            radarWall.transform.position = radarWallOriginalPos;
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

    public void active(bool act)
    {
        if(timer >= 10)
        {
            activated = act;
        }
    }
}
