using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject hinge;
    public void openDoor()
    {
        
        Destroy(this);
    }

}
