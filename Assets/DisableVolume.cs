using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class DisableVolume : MonoBehaviour
{
    public Volume v;
    // Start is called before the first frame update
    void Update()
    {
        Debug.Log("NoVolume = " + PlayerPrefs.GetInt("NoVolume"));
        if (v.enabled != (PlayerPrefs.GetInt("NoVolume") == 0))
        v.enabled = (PlayerPrefs.GetInt("NoVolume") == 0) ;
    }

}
