using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandRadarray : MonoBehaviour
{
    [SerializeField] private GameObject destinationEnd;
    private Vector3 startpoint;
    // Start is called before the first frame update
    void Start()
    {
        startpoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 50 * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == destinationEnd)
        {
            transform.position = startpoint;
        }
    }
}
