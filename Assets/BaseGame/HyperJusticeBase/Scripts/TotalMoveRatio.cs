using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TotalMoveRatio : MonoBehaviour
{
    public float totalMoveRatio;
    public float MR;
    public Text t;
    float startTime;
    void Start()
    {
        startTime = Time.realtimeSinceStartup;
        Camera.main.fieldOfView = 90;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup < startTime+1)
            Camera.main.fieldOfView = 90;
        MR = GetComponent<RobotController>().moveRatio-5;
        if (MR < 0)
            MR *= 3;
        totalMoveRatio += MR * Time.deltaTime;
        totalMoveRatio = Mathf.Max(0, Mathf.Min(5, totalMoveRatio));
        t.text = (int)(totalMoveRatio*10)/10+"";
    }
}
