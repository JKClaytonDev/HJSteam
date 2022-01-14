using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glass : MonoBehaviour
{
    public AudioClip[] sounds;
    public GameObject childSound;
    float startTime;
    private void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }
    private void OnCollisionEnter(Collision collision)
    {
        BreakGlass();
    }
    public void BreakGlass()
    {
        if (Time.realtimeSinceStartup < startTime - 3)
            return;
        childSound.transform.parent = null;
        Destroy(childSound, 2);
        childSound.GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.3f);
        childSound.GetComponent<AudioSource>().PlayOneShot(sounds[Random.Range(0, sounds.Length)], 0.6f);
        foreach (Transform t in transform)
        {
            t.gameObject.SetActive(true);
            t.parent = null;
        }
        Destroy(gameObject);
    }
}
