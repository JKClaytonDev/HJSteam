using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEgg : MonoBehaviour
{
    public GameObject enable;
    int step;
    

    // Update is called once per frame
    void Update()
    {
        if (step == 0 && Input.GetKeyDown(KeyCode.C))
            step = 1;
        if (step == 1 && Input.GetKeyDown(KeyCode.H))
            step = 2;
        if (step == 2 && Input.GetKeyDown(KeyCode.I))
            step = 3;
        if (step == 3 && Input.GetKeyDown(KeyCode.M))
            enable.SetActive(true);
    }
}
