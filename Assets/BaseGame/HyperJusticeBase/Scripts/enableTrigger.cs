using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enableTrigger : MonoBehaviour
{

    public Animator obj;
    public AudioClip sound;
    public GameObject soundObj;
    private void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.GetComponent<RobotController>())
            return;
        if (obj)
        obj.enabled = true;
        if (sound)
        {
            soundObj.transform.parent = null;
            soundObj.GetComponent<AudioSource>().PlayOneShot(sound);
            Destroy(soundObj, 5);
        }
        gameObject.SetActive(false);
    }
}
