using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    public GameObject upKey;
    public GameObject downKey;
    public GameObject rightKey;
    public GameObject leftKey;

    public void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            leftKey.SetActive(false);
        }
        else
        {
            leftKey.SetActive(true);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rightKey.SetActive(false);
        }
        else
        {
            rightKey.SetActive(true);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            upKey.SetActive(false);
        }
        else
        {
            upKey.SetActive(true);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            downKey.SetActive(false);
        }
        else
        {
            downKey.SetActive(true);
        }
    }
}