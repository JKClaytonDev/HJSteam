using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class VolumeCheckbox : MonoBehaviour
{
    public Toggle c;
    void Start()
    {
        c.isOn = PlayerPrefs.GetInt("NoVolume") == 0;
    }
    // Update is called once per frame
    void Update()
    {
        
        int box = 0;
        if (!c.isOn)
            box = 1;
        PlayerPrefs.SetInt("NoVolume", box);
        PlayerPrefs.Save();
    }
}
