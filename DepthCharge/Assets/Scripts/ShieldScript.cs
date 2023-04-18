using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour                           //script for shield ability, holds functions to enable shield
{
    [SerializeField] private GameObject player;
    [SerializeField] private AbilityScript pickup;                  //pickup orb
    private bool activated;
    private Vector3 restPos;

    private void Start()
    {
        restPos = transform.position;
        pickup.ability = ShieldActivated;                           //the base ability has its delegate set to shield
    }

    private void Update()
    {
        if (activated)
        {
            transform.position = player.transform.position;         //shield orb follows the player
        }
    }

    public void ShieldActivated()
    {
        activated = true;
    }

    public void ShieldDeactivate()
    {
        activated = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hit"))                 //if shield is hit, the shield is sent back to resting position, the same is also done for the obstacle that hit the shield
        {
            collision.gameObject.GetComponent<EnviormentMovement>().returnToRest();
            transform.position = restPos;
            ShieldDeactivate();
        }
    }
}
