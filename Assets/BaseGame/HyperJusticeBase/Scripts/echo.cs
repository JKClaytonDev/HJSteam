    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class echo : MonoBehaviour
{
    public float dist = -1;
    public bool update;
    GameObject player;
    float updTime;
    public void Start()
    {
        if (!FindObjectOfType<FPSMovement>())
            player = FindObjectOfType<RobotController>().gameObject;
        else if (!FindObjectOfType<RobotController>())
            player = FindObjectOfType<FPSMovement>().gameObject;
    }
    public void Update()
    {
        if (!player)
            player = FindObjectOfType<RobotController>().gameObject;
        if (!update || Time.realtimeSinceStartup < updTime)
            return;
        updTime = Time.realtimeSinceStartup + 1;
        if (dist != -1)
        {
            GetComponent<AudioSource>().enabled = (Vector3.Distance(transform.position, player.transform.position) < dist);
        }
        RaycastHit hit;
        GetComponent<AudioSource>().volume = Mathf.MoveTowards(GetComponent<AudioSource>().volume, 0.3f, Time.deltaTime);
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position) / 25, out hit))
        {
            GetComponent<AudioLowPassFilter>().cutoffFrequency = 12000 / (Vector3.Distance(transform.position, Camera.main.transform.position) / 3);
            if (hit.transform.gameObject == player)
            {
                GetComponent<AudioSource>().volume = Mathf.MoveTowards(GetComponent<AudioSource>().volume, 2, Time.deltaTime);
                GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000;
            }
        }
        GetComponent<AudioSource>().panStereo = (Camera.main.WorldToScreenPoint(transform.position).x - Screen.width / 2) / (Screen.width / 2);
    }
    public void playSound(AudioClip cl)
    {
        GetComponent<AudioSource>().volume = 15 / Mathf.Pow(Vector3.Distance(transform.position, Camera.main.transform.position), 2);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position) / 25, out hit))
        {
            GetComponent<AudioLowPassFilter>().cutoffFrequency = 12000 / (Vector3.Distance(transform.position, Camera.main.transform.position) / 3);
            if (hit.transform.gameObject == player)
            {
                GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000;
            }
        }
        GetComponent<AudioSource>().panStereo = (Camera.main.WorldToScreenPoint(transform.position).x - Screen.width / 2) / (Screen.width / 2);
        GetComponent<AudioSource>().PlayOneShot(cl);
    }
    public void playSound(AudioClip cl, float vol)
    {
        GetComponent<AudioSource>().volume = (15 / Mathf.Pow(Vector3.Distance(transform.position, Camera.main.transform.position), 2)) *vol;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (player.transform.position - transform.position) / 25, out hit))
        {
            GetComponent<AudioLowPassFilter>().cutoffFrequency = 12000 / (Vector3.Distance(transform.position, Camera.main.transform.position) / 3);
            if (hit.transform.gameObject == player)
            {
                GetComponent<AudioLowPassFilter>().cutoffFrequency = 22000;
            }
        }
        GetComponent<AudioSource>().panStereo = (Camera.main.WorldToScreenPoint(transform.position).x - Screen.width / 2) / (Screen.width / 2);
        GetComponent<AudioSource>().PlayOneShot(cl);
    }
}
