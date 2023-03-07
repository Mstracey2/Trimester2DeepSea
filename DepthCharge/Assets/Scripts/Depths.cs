using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "depths", menuName = "Create Depth", order = 1)]
public class Depths : ScriptableObject          // depths scriptable object, holds all the values for each level
{
    public float nextLevelTarget;               // depth the player needs to reach in order to transition level
    public string cameraBackgroundColour;       // background colour
    public float depthDensity;                  // density of the fog
    public int smallFishChance;                 // chance of a small fish spawning each frame
    public int mammelChance;                    // chance of a big fish spawning each frame
    public int speedMultiplier;                 // multiplier of the obstacle speed
    public int abilitesChance;                  //chance of an ability spawning each frame
}
