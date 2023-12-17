using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mimictext : MonoBehaviour
{
    public GameObject text;

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMesh>().text = text.GetComponent<TextMesh>().text;
    }
}
