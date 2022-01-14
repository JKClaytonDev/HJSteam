using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class enemyText : MonoBehaviour
{
    public string[] messages;
    bool triggered;
    void Update()
    {

        if (!triggered && Time.realtimeSinceStartup > 3)
        {
            Debug.Log("CHANGED");
            triggered = true;
            if (FindObjectOfType<RobotController>())
            {
                if (FindObjectOfType<RobotController>().partner)
                    Destroy(gameObject);
            }
            GetComponent<TextMesh>().text = messages[Random.Range(0, messages.Length - 1)];
            Destroy(gameObject, 1);
        }
    }


}
