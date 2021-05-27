using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScrps2 : MonoBehaviour
{
    public bool keyDown;
    private void Start()
    {
        keyDown = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            keyDown = true;
        }
    }
    void GetMessages(TimeInspctor timeInspctor)
    {
        if (keyDown)
        {
            timeInspctor.getFunc = true;
        }
        else
        {
            timeInspctor.getFunc = false;
        }
    }
}
