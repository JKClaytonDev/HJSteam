using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedImage : MonoBehaviour
{
    public Animator player;
    Rigidbody rb;
    public Sprite[] icons;
    public Sprite[] fireIcons;
    public Image main;
    int fireSprite = 0;
    public string[] animatorNames;
    public float avgDistance;
    Vector3 lastPos;
    float checkTime;
    int distanceSprite;
    public float dist;
    // Start is called before the first frame update
    void Start()
    {
        lastPos = transform.position;
        player = FindObjectOfType<RobotController>().GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup < checkTime)
            return;
        dist = (Vector3.Distance(rb.velocity, new Vector3()))/50;

            if (dist < avgDistance)
                avgDistance = Mathf.MoveTowards(avgDistance, dist, Time.deltaTime/1.5f);
            else
            avgDistance = Mathf.MoveTowards(avgDistance, dist, Time.deltaTime);
        lastPos = transform.position;
            fireSprite++;
            if (fireSprite > 3)
                fireSprite = 0;
        checkTime = Time.realtimeSinceStartup + 0.1f;
        avgDistance = (player.GetComponent<RobotController>().fovIncrease*3f) * Mathf.Min(1, dist*25);
        distanceSprite = (int)(avgDistance);
        if (distanceSprite < 3)
        main.sprite = icons[distanceSprite];
        else
            main.sprite = fireIcons[fireSprite];
    }
}
