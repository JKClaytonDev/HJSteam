using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class autoTest : MonoBehaviour
{
    public stoptesting t;
    public Button b;
    // Start is called before the first frame update
    void Start()
    {
        this.enabled = false;
        this.enabled = FindObjectOfType<MapData>().playMode;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!GameObject.Find("Player(Clone)"))
        {
            b.onClick.Invoke();
        }
    }
}
