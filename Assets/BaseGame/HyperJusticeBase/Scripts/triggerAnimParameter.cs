using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerAnimParameter : MonoBehaviour
{
    public GameObject child;
    public int speed = 1;
    public bool Play;
    public Animator anim;
    public string title;
    public int number;
    private void Start()
    {

        GetComponent<MeshRenderer>().enabled = false;

    }
    private void OnTriggerStay(Collider other)
    {
        if (Play)
        {
            child.transform.parent = null;
            Debug.Log("TRIGGERED");
            anim.Play(title);
            Destroy(gameObject);
        }
        if (!other.gameObject.GetComponent<RobotController>())
            return;
        if (Time.timeScale == 0)
            Time.timeScale = 1;
        anim.speed = speed;
        anim.SetInteger(title, number);
        Destroy(gameObject);
    }
}
