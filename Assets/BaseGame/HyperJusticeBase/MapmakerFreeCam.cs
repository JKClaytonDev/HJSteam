using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapmakerFreeCam : MonoBehaviour
{
    float speed = 5;
    public bool unLock;
    public bool toggle;
    // Update is called once per frame
    void Update()
    {
        toggle = !Input.GetMouseButton(1);
        if (toggle)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            //transform.position += transform.forward * Input.mouseScrollDelta.y * 180 * Time.deltaTime;
            return;
        }
        if (Input.GetMouseButton(1))
            GetComponent<MapMakerCamera>().selectedObject = null;
        
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        speed += Input.mouseScrollDelta.y * 3;
        speed = Mathf.Min(8, Mathf.Max(0.5f, speed));
        transform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.position += speed * (Input.GetAxis("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right) / 60;
    }
}
