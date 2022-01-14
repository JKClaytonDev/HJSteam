using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoDestroy2 : MonoBehaviour
{
    private float destroytime;
    public float whentodie;
    // Use this for initialization
    void OnEnable()
    {
        destroytime = Time.realtimeSinceStartup + whentodie;
        
    }

    void Update()
    {
        if (Time.realtimeSinceStartup > destroytime)
            gameObject.SetActive(false);
    }
}
