using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private AbilityScript pickup;
    private bool activated;
    private Vector3 restPos;
    private void Start()
    {
        restPos = transform.position;
        pickup.ability = ShieldActivated;
    }

    private void Update()
    {
        if (activated)
        {
            transform.position = player.transform.position;
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
        if (collision.gameObject.CompareTag("Hit"))
        {
            collision.gameObject.GetComponent<EnviormentMovement>().returnToRest();
            transform.position = restPos;
            ShieldDeactivate();
        }
    }
}
