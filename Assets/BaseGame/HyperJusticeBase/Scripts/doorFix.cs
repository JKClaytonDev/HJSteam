using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorFix : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnEnable()
    {
        foreach (DoorFrame f in FindObjectsOfType<DoorFrame>())
        {
           
            Door d = f.d;
            d.gameObject.SetActive(false);
            Debug.Log("DOOR ENABLED");
            if (FindObjectOfType<RobotController>())
            {
                d.GetComponent<doorHitEnemy>().enabled = false;
                d.transform.parent = null;
                if (!GetComponent<RobotController>().partner)
                {
                    GameObject k = Instantiate(d.hinge);
                    k.transform.parent = null;
                    d.transform.parent = null;
                    d.hinge.transform.position = d.transform.position;
                    d.hinge.transform.eulerAngles = d.transform.eulerAngles;
                    d.hinge.transform.Rotate(0, 180, 0);
                    d.hinge.GetComponent<removeChildren>().detach();
                    Destroy(d.gameObject);
                }
                else
                {
                    d.gameObject.SetActive(true);
                }
            }
        }
    }
}
