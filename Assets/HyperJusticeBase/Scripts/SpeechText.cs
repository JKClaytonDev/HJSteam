using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (SpeechText t in FindObjectsOfType<SpeechText>())
        {
            if (t.gameObject.activeInHierarchy && t != this)
            {
                Destroy(t.gameObject);
            }
        }
    }

}
