using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacePlayer : MonoBehaviour
{
    GameObject g;
    // Start is called before the first frame update
    void Start()
    {
        g = Camera.main.gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(g.transform);
    }
}
