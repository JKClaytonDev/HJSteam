using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplayerWeaponEquip : MonoBehaviour
{
    public GameObject child;
    public float activeTime;
    public int index;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.GetComponent<RobotController>() || Time.realtimeSinceStartup < activeTime)
            return;
        other.gameObject.GetComponent<RobotController>().equipped = index;
        activeTime = Time.realtimeSinceStartup + 15;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 90 * Time.deltaTime, 0);
        child.SetActive(Time.realtimeSinceStartup > activeTime);
    }
}
