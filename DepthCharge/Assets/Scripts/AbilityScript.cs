using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScript : MonoBehaviour                  // base script for abilites, runs a function when the ability hits the player
{
    public delegate void AbilityFunction();                 // delegate to hold the function that is accosiated to the ability
    public AbilityFunction ability;         
    private EnviormentMovement abilityOrb;

    private void Start()
    {
        abilityOrb = GetComponent<EnviormentMovement>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && ability != null)
        {
            abilityOrb.returnToRest();                      // ability orb goes back to its resting position when picked up, moves the orb out of play
            ability();                                      // runs the function attached to the delegate (will be the effect the ability has on the player)
            
        }
    }
}
