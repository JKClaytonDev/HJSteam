using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableAfterKey : MonoBehaviour
{
    public GameObject uzi;
    // Start is called before the first frame update
    void Start()
    {
        
        uzi.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKey("1") || Input.GetKey("2") || Input.GetKey("3"))
        {
            uzi.SetActive(false);
            this.enabled = false;
        }
    }
}
