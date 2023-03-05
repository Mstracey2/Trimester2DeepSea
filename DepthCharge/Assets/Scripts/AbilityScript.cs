using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityScript : MonoBehaviour
{
    public delegate void AbilityFunction();
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
            abilityOrb.returnToRest();
            ability();
            
        }
    }
}
