using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class propSound : MonoBehaviour
{
    public bool playOnce;
    public float Volume = 0.7f;
    public AudioClip[] clips;
    GameObject player;
    float updateTime;
    float startTime;
    Rigidbody rb;
    private void Start()
    {
        updateTime = Random.Range(0.1f  , 0.25f);
        startTime = Time.realtimeSinceStartup;
        rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<RobotController>().gameObject;
    }
    private void Update()
    {
        if (Time.realtimeSinceStartup < updateTime)
            return;
        updateTime += 0.3f;
        bool dist = Vector3.Distance(transform.position, player.transform.position) < 15 || Time.realtimeSinceStartup < startTime + 5;
        rb.useGravity = dist;
        rb.detectCollisions = dist;
    }
    void OnCollisionEnter(Collision collision)
    {
        if (playOnce)
        {
            if (!collision.gameObject.GetComponent<RobotController>())
                return;
        }
        if (Vector3.Distance(transform.position, Camera.main.transform.position) < 15)
        {
            Debug.Log("PLAYED COL");
            try
            {
                GetComponent<AudioSource>().PlayOneShot(clips[Random.Range(0, clips.Length - 1)], Mathf.Min(0.3f, Volume / 2));
            }
            catch
            {
                return;
            }
            if (playOnce)
                Destroy(this);
            }
    }
}
