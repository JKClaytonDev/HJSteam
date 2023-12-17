using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAimCam : MonoBehaviour
{
    float mouseSensValue;
    RobotController controller;
    public bool canLook = true;
    public GameObject player;
    public Animator anim;
    public float recoil;
    float lastRecoil;
    float fixedRecoil;
    public float[] recoilLevel;
    public float[] recoilSpeed;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<RobotController>();
        mouseSensValue = PlayerPrefs.GetFloat("SENS");
        if (!FindObjectOfType<enableItem>())
            canLook = true;
        GetComponent<Camera>().fieldOfView = 80;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canLook)
            return;
        float rot = Input.GetAxis("Mouse X") + Input.GetAxis("JoystickX");
        
        transform.Rotate(0, rot * (mouseSensValue / 50), 0);
        Vector3 angles = transform.localEulerAngles;
        angles.z = 0;
        angles.x = 0;
        if (Input.GetKey(KeyCode.C))
        {
            angles.x = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("DiveContd"))
        {

            angles.x = 0;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide") || anim.GetCurrentAnimatorStateInfo(0).IsName("SlideLeftRoll") || anim.GetCurrentAnimatorStateInfo(0).IsName("SlideLeftRoll"))
        {

            player.transform.localEulerAngles += angles;
            angles = new Vector3();
        }
        else
        {
            player.transform.localEulerAngles += angles;
                angles = new Vector3();
            
        }
        angles.x += lastRecoil*recoilLevel[controller.equipped];
        angles.x -= fixedRecoil* recoilLevel[controller.equipped];
        recoil /= 1+(Time.deltaTime*2);
        float vert = Input.GetAxis("Mouse Y")-Input.GetAxis("JoystickY");
        transform.localEulerAngles = angles + new Vector3(transform.localEulerAngles.x-vert * (mouseSensValue / 50), 0, 0);
        lastRecoil = fixedRecoil;
        fixedRecoil = Mathf.MoveTowards(fixedRecoil, recoil, Time.deltaTime * recoilSpeed[controller.equipped]);
    }
}
