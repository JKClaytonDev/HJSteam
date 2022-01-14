using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
public class MultiplayerCamera : MonoBehaviour
{
    public CapsuleCollider cc;
    public NetworkTransform t;
    public Camera c;
    public RobotController rc;
    public Canvas cv;
    public PlayerAimCam pac;
    public GameObject usable;
    public float killTime;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<CapsuleCollider>().enabled = false;
        cc.enabled = true;
        
        if (!t.isLocalPlayer)
        {
            if (c)
                c.enabled = false;
            if (rc)
                rc.enabled = false;
            if (cv)
                cv.enabled = false;
            if (pac)
                pac.enabled = false;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Update()
    {
        usable.gameObject.SetActive(Time.realtimeSinceStartup > killTime);
    }

}
