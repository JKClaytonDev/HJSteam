using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MouseSens : MonoBehaviour
{
    public Scrollbar scroll;
    public Text current;
    float scrollValue;
    float maxSens = 150;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("SENS"))
            PlayerPrefs.SetFloat("SENS", maxSens/3);
        scroll.value = PlayerPrefs.GetFloat("SENS")/ maxSens;
    }

    // Update is called once per frame
    void Update()
    {
        current.text = Mathf.Round(scroll.value*1000)/10+"";
        PlayerPrefs.SetFloat("SENS", Mathf.Round(scroll.value* maxSens * 10) /10);
        PlayerPrefs.Save();
    }
}
