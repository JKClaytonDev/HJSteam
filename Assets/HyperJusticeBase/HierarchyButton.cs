using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HierarchyButton : MonoBehaviour
{
    public string text;
    public Text t;
    // Start is called before the first frame update
    public void clickButton()
    {
        FindObjectOfType<MapMakerCamera>().selectedObject = GameObject.Find(text);
    }
    // Update is called once per frame
    void Update()
    {
        t.text = text;
    }
}
