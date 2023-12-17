using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCrosshair : MonoBehaviour
{
    float mouseSensValue;
    Vector3 targetPos;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        mouseSensValue = PlayerPrefs.GetFloat("SENS");
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().position = new Vector3(Screen.width / 2, Screen.height / 2, 0) + targetPos;// Vector3.MoveTowards(GetComponent<RectTransform>().position, targetPos, Time.deltaTime*10);

        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DiveBack"))
            targetPos += new Vector3((Input.GetAxis("Mouse X"))*5*(mouseSensValue/50), 0, 0);
        else
            targetPos = new Vector3();


            }
}
