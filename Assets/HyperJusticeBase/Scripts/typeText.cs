using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class typeText : MonoBehaviour
{
    public AudioClip sound;
    string text = "";
    public string targetText = "";
    string lasttt;
    float ltt;
    // Update is called once per frame
    void Update()
    {
        if (targetText != lasttt)
            text = "";
        if (Time.realtimeSinceStartup > ltt && text.Length < targetText.Length)
        {
            GetComponent<AudioSource>().pitch = Random.Range(0.5f, 1.5f);
            if (text != targetText)
                GetComponent<AudioSource>().PlayOneShot(sound);
            text += targetText.ToCharArray()[text.Length];
            ltt = Time.realtimeSinceStartup + 0.1f;
        }
        GetComponent<Text>().text = text;
        lasttt = targetText;
    }
}
