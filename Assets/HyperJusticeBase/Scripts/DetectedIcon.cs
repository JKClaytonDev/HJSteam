using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DetectedIcon : MonoBehaviour
{
    public RobotController player;
    
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().enabled = player.moveRatio < 5;
    }
}
