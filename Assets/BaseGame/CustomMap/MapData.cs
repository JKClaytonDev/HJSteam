using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public bool playMode;
    public string mapData;
    public string fullString; private void Awake()
    {
        if (FindObjectsOfType<MapData>().Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void setPlayMode(bool b)
    {
        playMode = b;
    }

}
