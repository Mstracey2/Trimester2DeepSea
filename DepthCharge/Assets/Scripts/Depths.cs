using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "depths", menuName = "Create Depth", order = 1)]
public class Depths : ScriptableObject
{
    public float nextLevelTarget;
    public string cameraBackgroundColour;
    public float depthDensity;
    public int smallFishChance;
    public int mammelChance;
    public int speedMultiplier;
    public int abilitesChance;
}
