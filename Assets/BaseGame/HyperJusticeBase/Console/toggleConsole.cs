using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toggleConsole : MonoBehaviour
{
    public GameObject essentials;
    public GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        transform.parent = null;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            child.SetActive(!child.activeInHierarchy);
            Time.timeScale = 1;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            if (FindObjectOfType<RobotController>())
            FindObjectOfType<RobotController>().enabled = !child.activeInHierarchy;
        }
        
        if (child.activeInHierarchy)
        {
            if (!FindObjectOfType<settingsMenu>())
            {
                child.SetActive(!child.activeInHierarchy);
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                if (FindObjectOfType<RobotController>())
                    FindObjectOfType<RobotController>().enabled = !child.activeInHierarchy;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            
        }


    }
}
