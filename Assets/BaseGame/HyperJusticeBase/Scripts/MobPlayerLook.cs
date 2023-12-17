using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobPlayerLook : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<RobotController>().gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        transform.Rotate(0, 90, 0);
    }
}
