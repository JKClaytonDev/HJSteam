using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SoundScrollbar : MonoBehaviour
{
    public Scrollbar scroll;
    public Text current;
    float scrollValue;

    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("VOL"))
            PlayerPrefs.SetFloat("VOL", 100);
        scroll.value = Mathf.Min(1, PlayerPrefs.GetFloat("VOL"));
    }

    // Update is called once per frame
    void Update()
    {
        current.text = Mathf.Round(scroll.value * 1000) / 10 + "";
        PlayerPrefs.SetFloat("VOL", scroll.value);
        PlayerPrefs.Save();
        AudioListener.volume = scroll.value;
    }
}
