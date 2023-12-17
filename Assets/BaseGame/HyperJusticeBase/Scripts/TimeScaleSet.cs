using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleSet : MonoBehaviour
{
    public void setTime()
    {
        FindObjectOfType<RobotController>().enabled = true;
        Cursor.visible = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }
    // Update is called once per frame
    void Update()
    {
        FindObjectOfType<RobotController>().enabled = false;
        Cursor.visible = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }
}
