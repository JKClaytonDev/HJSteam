using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMoveRatio : MonoBehaviour
{
    public RobotController player;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(player.moveRatio, 1, 1);
    }
}
