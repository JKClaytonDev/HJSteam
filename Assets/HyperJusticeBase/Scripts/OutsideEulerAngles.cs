using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideEulerAngles : MonoBehaviour
{
    float mouseSensValue;
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        mouseSensValue = PlayerPrefs.GetFloat("SENS");
        parent = transform.parent.gameObject;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = parent.gameObject.transform.position;
        Vector3 TEU = FindObjectOfType<RobotController>().transform.eulerAngles;
        TEU.x = 0;
        TEU.z = 0;
        transform.eulerAngles = TEU + new Vector3(transform.eulerAngles.x-Input.GetAxisRaw("Mouse Y") * (mouseSensValue / 50), 0, 0);

    }
}
