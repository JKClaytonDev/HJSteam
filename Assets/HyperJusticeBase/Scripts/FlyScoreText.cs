using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform);
        transform.position += transform.up*Time.deltaTime;
        transform.localScale /= 1 + (Time.deltaTime / 1);
    }
}
