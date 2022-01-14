using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    float mouseSensValue;
    float cameraFOV;
    Camera cam;
    float zRot;
    Vector3 lastPositon;
    public RobotController player;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        mouseSensValue = PlayerPrefs.GetFloat("SENS");
    }

    // Update is called once per frame
    void Update()
    {
        float vert = -Input.GetAxis("Mouse Y");
        transform.Rotate(vert * (mouseSensValue / 50), 0, 0);
        lastPositon = transform.position;
    }
}
