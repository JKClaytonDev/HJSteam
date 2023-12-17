using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class videoEnd : MonoBehaviour
{
    float startTime;
    private void Start()
    {
        startTime = Time.realtimeSinceStartup;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup > startTime+10 && !GetComponent<VideoPlayer>().isPlaying)
        {
            Debug.Log("VIDEO DONE");
            SceneManager.LoadScene("MainMenu");
        }
    }
}
