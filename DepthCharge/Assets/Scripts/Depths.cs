using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "depths", menuName = "Create Depth", order = 1)]
public class Depths : ScriptableObject
{
    public float obstacleSpeed;
    public float nextLevelTarget;
    public string cameraBackgroundColour;
}
