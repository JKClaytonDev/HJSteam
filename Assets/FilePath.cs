using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FilePath : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<MapData>())
        GetComponent<Text>().text = FindObjectOfType<MapData>().mapData;
    }


}
