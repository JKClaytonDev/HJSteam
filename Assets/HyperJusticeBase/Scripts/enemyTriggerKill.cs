using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyTriggerKill : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<gameEnemy>())
        {
            FindObjectOfType<RobotController>().HitEnemy(other.gameObject);
            FindObjectOfType<RobotController>().fireGun();
        }
    }
}
