using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleCamera : MonoBehaviour
{

    float mouseSensValue;
    Camera cam;
    private void Start()
    {
        mouseSensValue = PlayerPrefs.GetFloat("SENS");
    }
    // Update is called once per frame
    void Update()
    {

        if (!cam)
        {
            cam = GetComponent<Camera>();
            cam.enabled = false;
        }
        transform.Rotate(-Input.GetAxis("Mouse Y") * (mouseSensValue / 50), 0, 0);
        if (Input.GetKeyDown(KeyCode.Tab))
            cam.enabled = !cam.enabled;
    }
}
