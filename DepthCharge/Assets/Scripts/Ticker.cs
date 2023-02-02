using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ticker : MonoBehaviour
{
    public ScrollCrate scrollCrate;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        scrollCrate.closestButton = collision.gameObject;
    }
}
