using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableItem : MonoBehaviour
{
    public bool disableLook;
    public bool enableLook;
    public Vector3 angles;
    public GameObject[] disable;
    // Start is called before the first frame update
    private void Start()
    {
        if (disableLook && FindObjectOfType<PlayerAimCam>())
            FindObjectOfType<PlayerAimCam>().canLook = false;
        foreach (GameObject g in disable)
            g.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (enableLook)
        {
            FindObjectOfType<PlayerAimCam>().canLook = false;
            if (!FindObjectOfType<PlayerAimCam>().canLook)
                FindObjectOfType<RobotController>().transform.localEulerAngles = angles;
        }
        if (disableLook) { 
            FindObjectOfType<PlayerAimCam>().canLook = true;
            
            }
        foreach (activeTrigger t in FindObjectsOfType<activeTrigger>())
            t.gameObject.SetActive(false);
        foreach (GameObject g in disable)
        {
            if (g != null)
            g.SetActive(true);
        }

    }
}
