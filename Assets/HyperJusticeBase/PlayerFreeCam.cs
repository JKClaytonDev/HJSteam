using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerFreeCam : MonoBehaviour
{
    float speed = 1;
    public bool unLock;
    public bool toggle;
    // Update is called once per frame
    void Update()
    {
        if (!unLock)
        {
            if (Input.GetKey(KeyCode.Escape))
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            if (FindObjectOfType<RobotController>())
                FindObjectOfType<RobotController>().gameObject.SetActive(false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            toggle = !Input.GetMouseButton(1);
            if (toggle)
            {
                
                GetComponent<MapMakerCamera>().selectedObject = null;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        speed += Input.mouseScrollDelta.y/3;
        speed = Mathf.Min(8, Mathf.Max(0.5f, speed));
        transform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);
        transform.position += speed * (Input.GetAxis("Vertical") * transform.forward + Input.GetAxisRaw("Horizontal") * transform.right) / 60;
    }
}
