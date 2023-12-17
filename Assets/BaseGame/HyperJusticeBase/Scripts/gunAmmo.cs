using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gunAmmo : MonoBehaviour
{
    public AudioClip unload;
    public AudioClip reload;
    public GameObject clip;
    public GameObject clipSpot;
    public RobotController r;
    public Text t;
    public Image i;
    float Ammo;
    float step = 0;
    public Text AmmoCounter;
    public Image AmmoBar;
    // Update is called once per frame

    void Update()
    {
        float scroll = Input.GetAxis("JoystickReload") + Input.mouseScrollDelta.y;
        AmmoCounter.text = "" + r.ammo;
        if (step == 0)
        {
            t.text = "Scroll DOWN to unload your gun";

        }
        if (step == 0 && scroll < 0 && r.ammo != r.startAmmo)
        {
            r.ammo = 0;
            step = 1;
            t.text = "Scroll UP to load a clip";
            r.GetComponent<Animator>().Play("Unload", 3);
            GetComponent<AudioSource>().PlayOneShot(unload);
            GameObject p = Instantiate(clip);
            p.transform.position = clipSpot.transform.position;
        }
        if (step == 1 && scroll > 0)
        {
            step = 0;
            r.ammo = r.startAmmo;
            GetComponent<AudioSource>().PlayOneShot(reload);
            r.GetComponent<Animator>().Play("Reload", 3);
        }
        
        if (r.ammo == 0 || step == 1)
        {
            i.gameObject.SetActive(true);
            
        }
        else
        {
            i.gameObject.SetActive(false);
        }
        AmmoBar.transform.localScale = new Vector3(r.ammo / r.startAmmo, 1, 1);
    }
}
