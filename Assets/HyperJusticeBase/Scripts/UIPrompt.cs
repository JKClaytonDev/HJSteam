using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPrompt : MonoBehaviour
{
    public KeyCode waitforKey;
    public Animator waitforAnim;
    public int layer;
    public string animName;
    public int time = -1;
    // Start is called before the first frame update
    void Start()
    {
        if (time != -1)
            Destroy(gameObject, time);
    }
    // Update is called once per frame
    void Update()
    {
        foreach (UIPrompt k in GameObject.FindObjectsOfType<UIPrompt>())
        {
            if (k.gameObject.activeInHierarchy == true && k != this)
                Destroy(k.gameObject);
        }
        if (Input.GetKey(waitforKey))
            Destroy(gameObject);
        if (waitforAnim)
        {
            if (waitforAnim.GetCurrentAnimatorStateInfo(layer).IsName(animName))
                Destroy(gameObject);
        }
    }
}
