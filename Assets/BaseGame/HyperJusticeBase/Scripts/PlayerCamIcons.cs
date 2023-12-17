using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamIcons : MonoBehaviour
{
    public Camera parentCam;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
        cam.clearFlags = CameraClearFlags.Nothing;
    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = parentCam.fieldOfView;
    }
}
