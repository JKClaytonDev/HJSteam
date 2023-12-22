using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class RobotController : MonoBehaviour
{
    Vector3 startPos;
    Vector3 startRot;
    public Image IMG;
    public bool partner;
    bool lastDodge;
    public float fovIncrease;
    bool sliding;
    float randomMR;
    float lastRagdoll;
    public LayerMask hitLayers;
    public GameObject rotCam;
    public float fireTime;
    float shootTime;
    public GameObject bulletLight;
    public GameObject cam;
    public GameObject crosshair;
    bool pauseVel;
    AnimationEvent flip;
    Rigidbody rb;
    public int equipped = 1;
    Animator anim;
    float xrot = 0;
    public LayerMask ground;
    float walldistance = 0;
    public bool camStatic;
    public bool onGround;
    public GameObject hand;
    Vector3 playerVel;
    GameObject lockonEnemy;
    public GameObject chest;
    public Image ratioSize;
    public GameObject sound;    
    float spinTime;
    AnimationEvent evt;
    GameObject attached;
    
    public GameObject ragDoll;
    public AudioClip[] killSound;
    public GameObject door;
    public Text health;
    public float playerhealth = 100;
    public int moveRatio;
    public float hitTime;
    public AudioClip[] gunSound;
    public LayerMask bullets;
    public Text comboCounter;
    public Image comboMeter;
    [HideInInspector]public float combo;
    public GameObject grenade;
    public GameObject[] nadeBelt;
    public GameObject bulletCol;
    float nadeCount = 3;
    public GameObject clone;
    Vector3 clonePos;
    Vector3 currentVel;
    public float lockSpeed = 1;
    Vector3 diveDir;
    bool crouchDown;    float crouchMoveTime;
    float slideTime;
    string lastMove;    Vector3 crouchStartPos;
    public LayerMask gunLayers;
    string[] rlMoves = { "KnifeLeap", "FlipForward", "DiveRoll"};
    string[] fwMoves = { "KnifeLeap", "FlipForward", "WallJump", "AxeSwing", "DiveContd", "DiveRoll"};
    string[] hzMoves = { "SideFlipLeftAnim", "SideFlipRightAnim", "Spin", "RightSpin", "SlideLeftRoll", "SlideRightRoll" };
    public GameObject settings;
    public GameObject trailL;
    public GameObject trailR;
    float T1AT;
    bool startGun;
    public float rayDistances;
    public float[] gunAmmos;
    public float startAmmo;
    public float ammo;
    bool started = false;
    PlayerAimCam k;
    public GameObject dead;
    public AudioClip dieSound;
    float difficulty;
    public GameObject dodgeMeter;
    float dodgeTime;
    public bool partnerlevel;
    public GameObject anti;
    public GameObject FPSCamera;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("CliveFPS", 0);
        PlayerPrefs.Save();
        if (!partner && PlayerPrefs.GetInt("CliveFPS") == 1)
        {
            Camera.main.enabled = false;
            FPSCamera.SetActive(true);
        }
        if (PlayerPrefs.HasKey("Partner") && (PlayerPrefs.GetInt("Partner") == 1) != partner)
        {
            foreach (triggerAnimParameter t in FindObjectsOfType<triggerAnimParameter>())
                Destroy(t.gameObject);
            foreach (enableItem t in FindObjectsOfType<enableItem>())
            {
                if (t.disableLook || t.enableLook)
                Destroy(t.gameObject);
            }
            GameObject k = Instantiate(anti);
            k.transform.position = transform.position;
            Destroy(gameObject);

        }
        dodgeTime = Mathf.Sqrt(SceneManager.GetActiveScene().buildIndex) / 5;
        difficulty = PlayerPrefs.GetFloat("Difficulty");
        AudioListener.volume = Mathf.Min(1, PlayerPrefs.GetFloat("VOL"));
        Time.timeScale = 1;
        startPos = transform.position;
        startRot = transform.localEulerAngles;
        if (FindObjectOfType<SetGun>())
        {
            equipped = FindObjectOfType<SetGun>().startGun;
            startGun = true;
        }
        startAmmo = gunAmmos[FindObjectOfType<SetGun>().startGun];
        randomMR = Random.Range(0.3f, 1.3f);
        k = FindObjectOfType<PlayerAimCam>();
        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        clone.transform.parent = null;

        playerhealth = 100;
        flip = new AnimationEvent();
        flip.functionName = "PlayerFlip";
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        anim = GetComponent<Animator>();
        evt = new AnimationEvent();
        evt.functionName = "kickDoor";
        anim.SetInteger("Equipped", equipped);
        anim.Play("Zoom");

        if (PlayerPrefs.GetInt("AutoHUD") == 1 || PlayerPrefs.GetInt("LockHUD") == 1)
        {
            foreach (Canvas c in FindObjectsOfType<Canvas>())
            {
                if (c != this)
                    c.enabled = false;
            }
            foreach (triggerAnimParameter c in FindObjectsOfType<triggerAnimParameter>())
            {
                c.enabled = false;
                Destroy(c.GetComponent<BoxCollider>());
            }
            foreach (activeTrigger c in FindObjectsOfType<activeTrigger>())
            {
                c.enabled = false;
                Destroy(c.GetComponent<BoxCollider>());
            }
            foreach (enableItem c in FindObjectsOfType<enableItem>())
            {
                c.enabled = false;
                Destroy(c.GetComponent<BoxCollider>());
            }
        }
    }
    public void PlayerFlip()
    {
        pauseVel = false;
        transform.Rotate(new Vector3(0, 180, 0));
    }
    public void kickDoor()
    {

        attached.transform.parent = null;
        GameObject k = Instantiate(door);
        k.transform.localScale /= 2;
        k.transform.position = attached.transform.position;
        Vector3 aPos = attached.transform.position;
        Destroy(attached.GetComponent<Door>());
        attached.GetComponent<doorHitEnemy>().enabled = true;
        attached.layer = 14;
        attached.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        attached.GetComponent<Rigidbody>().velocity = transform.forward * 255;
    }
    public void HitPlayer(GameObject hit2)
    {
        if (Time.realtimeSinceStartup < lastRagdoll)
            return;
        lastRagdoll = Time.realtimeSinceStartup + 0.05f;
        if (!hit2.gameObject.GetComponent<MultiplayerCamera>())
            return;
        combo++;
        GetComponent<AudioSource>().PlayOneShot(killSound[Random.Range(0, killSound.Length)]);
        hit2.transform.LookAt(transform);
        Vector3 pos = hit2.transform.gameObject.transform.position;
        Vector3 rot = hit2.transform.gameObject.transform.localEulerAngles;
        GameObject kk = Instantiate(ragDoll);
        float force = 3;
        foreach (Rigidbody rb in kk.GetComponent<StoreRBs>().list)
        {
            force = 3;
            if (Vector3.Distance(transform.position, kk.transform.position) < 15 && equipped == 2)
            {
                force = 15;
                Vector3 dir = Camera.main.transform.forward + transform.up * 0.1f;
                rb.velocity = force * dir;
            }
        }
        kk.transform.position = pos;
        kk.transform.localEulerAngles = rot;

    }
    public void HitEnemy(GameObject hit2)
    {
        if (Time.realtimeSinceStartup < lastRagdoll)
            return;
        lastRagdoll = Time.realtimeSinceStartup + 0.05f;
        if (!hit2.gameObject.GetComponent<gameEnemy>())
            return;
        combo++;
        GetComponent<AudioSource>().PlayOneShot(killSound[Random.Range(0, killSound.Length)]);
        hit2.transform.LookAt(transform);
        Vector3 pos = hit2.transform.gameObject.transform.position;
        Vector3 rot = hit2.transform.gameObject.transform.localEulerAngles;
        Destroy(hit2.transform.gameObject);
        GameObject kk = Instantiate(ragDoll);
        float force = 3;
        foreach (Rigidbody rb in kk.GetComponent<StoreRBs>().list)
        {
            force = 3;
            if (Vector3.Distance(transform.position, kk.transform.position) < 15 && equipped == 2)
            {
                force = 15;
                Vector3 dir = Camera.main.transform.forward + transform.up * 0.1f;
                rb.velocity = force * dir;
            }
        }
        kk.transform.position = pos;
        kk.transform.localEulerAngles = rot;
       
    }
    public void Reset()
    {
        anim.Play("PistolIdle");
    }
    public string randomAnim(string[] names)
    {
        return (names[Random.Range(0, names.Length)]);
    }
    // Update is called once per frame
    void Update()
    {
        if (partner)
        {
            transform.localScale = new Vector3(1, 1, 1);
            if (Input.GetKey(KeyCode.C))
                    transform.localScale = new Vector3(0, 0.2f, 0);
        }
        dodgeMeter.transform.localScale -= new Vector3(Time.deltaTime*dodgeTime, 0, 0);
        if (partner)
            dodgeMeter.transform.localScale = new Vector3(0.5f*3, 0, 0); 
        moveRatio = 1+(int)((dodgeMeter.transform.localScale.x)/3 * 5);
        if (dodgeMeter.transform.localScale.x < 0)
            dodgeMeter.transform.localScale = new Vector3(0, 3, 1);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        trailL.SetActive(Time.realtimeSinceStartup < T1AT && equipped == 3);
        trailR.SetActive(Time.realtimeSinceStartup < T1AT);
        if (ammo == 0 && !started)
        {
            GetComponent<Rigidbody>().velocity = new Vector3();
            transform.position = startPos;
            transform.localEulerAngles = startRot;
            transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
            return;
        }
        started = true;
        if (Input.GetKeyDown(KeyCode.V))
        {
            
            Collider[] hitColliders = Physics.OverlapSphere(transform.position+transform.forward, 2);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.gameObject.GetComponent<Rigidbody>() && hitCollider.gameObject != gameObject)
                {
                    anim.Play("KickDoor");
                    GameObject k = hitCollider.gameObject;
                    k.transform.localScale *= 0.9f;
                    k.transform.gameObject.transform.position += transform.up;
                    k.transform.gameObject.GetComponent<Rigidbody>().velocity = (transform.forward * 3 + transform.up) * 3;
                }
            }

        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            settings.SetActive(!settings.activeInHierarchy);
            Time.timeScale = 1;
        }

        float fovAddition = 0;
        bool isMoving = false;
        foreach (string s in fwMoves)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(s))
            {
                isMoving = true;
                fovAddition = 5;
            }
        }
        foreach (string s in hzMoves)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName(s))
            {
                isMoving = true;
                fovAddition = 15;
            }
        }
        if (isMoving && !lastDodge)
        {
            if (fovIncrease > 0.1f)
                fovIncrease = 0.5f;
            fovIncrease += 0.5f;
        }

        lastDodge = isMoving;
        if (!isMoving)
            fovIncrease -= (Time.deltaTime);
        if (fovIncrease < 0)
            fovIncrease = 0;
        Color color = Color.black;
        color.a = (25 - (fovIncrease * 15)) / 100;
        Debug.Log("A IS " + color.a);
        IMG.color = color;
        
        Camera.main.fieldOfView = (90 + SceneManager.GetActiveScene().buildIndex/3) + Mathf.Min(45, fovIncrease*15) + fovAddition;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide") && Random.Range(1, 10) > 3)
            isMoving = true;
        if (isMoving)
            dodgeMeter.transform.localScale = new Vector3(3, 3, 1);
        RaycastHit h;
        if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out h, 500, ground))
            transform.position = h.point;
        float vy = rb.velocity.y;
        RaycastHit dwn;
        float pt1;
        Physics.Raycast(transform.position, Vector3.down, out dwn, 500, ground);
        pt1 = dwn.point.y;
        Physics.Raycast(transform.position + transform.forward * 0.1f, Vector3.down, out dwn, 500, ground);
        rayDistances = pt1 - dwn.point.y;

        if (Input.GetKeyDown(KeyCode.E) && !partner)
        {
            anim.Play("AxeSwing");
        }
        if (!FindObjectOfType<gameEnemy>()) {
            if (FindObjectOfType<LoadScene>())
                SceneManager.LoadScene(FindObjectOfType<LoadScene>().SceneName);
            else if (!FindObjectOfType<PreventLevelSkip>())
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
            
        if (Input.GetKeyDown(KeyCode.E) && nadeCount > 0 && partner)
        {
            nadeCount--;
            for (int i = 0; i < 3; i++)
            {
                nadeBelt[i].SetActive(i <= nadeCount - 1);
            }
            anim.Play("ThrowGrenade", 2);
            GameObject k = Instantiate(grenade);
            k.transform.position = transform.position + Camera.main.transform.forward;

        }

        playerhealth += 15 * Time.deltaTime;
        playerhealth = Mathf.Min(100, playerhealth);
        //moveRatio = 1;

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("AxeSwing"))
        {
            //moveRatio = 2;
            anim.SetLayerWeight(2, 0);
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            return;
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("KickDoor"))
        {
            anim.SetLayerWeight(3, 0);
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 0);
            return;
        }
        if ((Input.GetAxis("Vertical") > 0))
            anim.SetInteger("Dir", 1);
        if ((Input.GetAxis("Vertical") < 0))
            anim.SetInteger("Dir", 2);
        if (Input.GetAxis("Horizontal") < 0)
            anim.SetInteger("Dir", 3);
        if ((Input.GetAxis("Horizontal") > 0))
            anim.SetInteger("Dir", 4);
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("RunFW"))
        {
            //moveRatio += 1;
            anim.SetLayerWeight(3, 1);
            if (!partner)
            {
                anim.Play("Sprint");
            }
        }
        else
            anim.SetInteger("Dir", 1);
        foreach (Door d in FindObjectsOfType<Door>())
        {
            if (Vector3.Distance(transform.position, d.transform.position) < 3)
            {
                if (!Input.GetKey(KeyCode.V) && !partner)
                {
                    d.openDoor();
                    return;
                }
                attached = d.gameObject;
                transform.LookAt(d.transform);
                Vector3 rot = transform.eulerAngles;
                rot.x = 0;
                rot.z = 0;
                transform.eulerAngles = rot;
                float y = transform.position.y;

                Vector3 pos = d.transform.position;
                if (y < pos.y)
                    pos.y = y;
                transform.position = pos;
                transform.position -= transform.forward * 0.727f;
                anim.Play("KickDoor");

            }
        }



        comboCounter.text = "" + combo;
            onGround = false;
            cam.name = "NOT THE CAMERA";
            if (pauseVel)
                cam.name = "Main Camera";

            
            RaycastHit wall;

            //if (!pauseVel)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.forward + Vector3.up, -Vector3.up, out hit, 3, ground))
                {
                    onGround = true;
                    transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal));
                }

            }
            if (!onGround)
            {
                RaycastHit hit;
                if (!Physics.Raycast(transform.position + transform.forward + Vector3.up, -Vector3.up, out hit, 5, ground))
                {
                    //moveRatio += 3;
                    anim.Play("LegsFall");
                    return;
                }

            }
            anim.SetBool("onGround", onGround);
            camStatic = false;


            crouchDown = false;
            anim.SetBool("Crouch", false);
            Vector3 vel = rb.velocity;
            Vector3 newVel = new Vector3();
            if (!partner && Input.GetKey(KeyCode.Space) && k.canLook)
            {
                anim.SetBool("Running", true);
                newVel = transform.forward * 12 * lockSpeed;
                if (!camStatic)
                    transform.localEulerAngles += new Vector3(0, Input.GetAxis("Horizontal") * Time.deltaTime * 0.3f, 0);

            }
            else
            {
                float vertical = Input.GetAxis("Vertical");
                float horizontal = Input.GetAxis("Horizontal");
                if (partner)
                {
                    horizontal*=1;
                    vertical *= 2;
                }
                if (!k.canLook)
                {
                    vertical = 0.7f;
                    horizontal = 0;
                }
                if (!partner)
                    vertical = 3;
                anim.SetBool("Running", horizontal != 0 || vertical != 0);
                if (horizontal != 0 || vertical != 0)
                    newVel = ((transform.forward * 4 * vertical) + (transform.right * 4 * horizontal)) * lockSpeed;
            }
            anim.speed = 1;
            anim.SetLayerWeight(2, 1);
            anim.SetLayerWeight(0, 1);
            anim.SetLayerWeight(1, 1);

            string aName = null;
            string foundAnim = "null";
            foreach (string s in fwMoves)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(s)) {
                    foundAnim = "fw";
                    aName = s;
                }
            }
            foreach (string s in hzMoves)
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName(s)) {
                    foundAnim = "hz";
                    aName = s;
                }
            }
            if (foundAnim == "fw")
            {
                moveRatio += 4;
                camStatic = true;
                newVel = transform.forward * 25 * lockSpeed;
            }
            else if (foundAnim == "hz")
            {
                moveRatio += 8;
                camStatic = true;
                int dir = -1;
                if (aName.Contains("Right"))
                    dir = 1;
                newVel = transform.right * dir * 14 * lockSpeed;
                if ((Input.GetAxis("DodgeVert") > 0))
                    newVel += transform.forward * 14;
                if (Physics.Raycast(transform.position, transform.right, 3) && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1)))
                {
                    anim.Play("RightWallJump");
                }
                else if (Physics.Raycast(transform.position, -transform.right, 3) && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(1)))
                {
                    anim.Play("WallJump");
                }

            }
            else if (Input.GetMouseButton(1))
            {

                newVel /= 1f;
                anim.speed = 0.9f;
            }
            if (aName != "")
                lastMove = aName;
            if (!onGround)
            {
                anim.SetLayerWeight(0, 1);
                anim.SetLayerWeight(1, 0);

            }
            vel.x = newVel.x;
            vel.z = newVel.z;
            if (pauseVel)
            {
                vel.x = 0;
                vel.z = 0;
            }
            playerVel = vel;
            moveRatio = (int)((float)moveRatio /difficulty);
        if (onGround)
            rb.velocity = playerVel;
        anim.SetInteger("Equipped", equipped);
        if (!startGun)
        {
            if (Input.GetKey("1"))
                equipped = 3;
            if (Input.GetKey("2"))
                equipped = 2;
            if (Input.GetKey("3"))
                equipped = 1;
        }
        bool fireKey = Input.GetMouseButton(0) || Input.GetKey(KeyCode.LeftControl);
        bool fireDown = Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.LeftControl);
        AnimatorStateInfo aInf = anim.GetCurrentAnimatorStateInfo(1);
        bool animPlaying = aInf.IsName("DualPistolsSprintFire") || aInf.IsName("DualPistolsFire") || aInf.IsName("StandStillShotgunFire") || aInf.IsName("FireShotgun") || aInf.IsName("StandStillUziFire") || aInf.IsName("FireUzi") || aInf.IsName("PistolFire");
        if ((fireDown || fireKey && Time.realtimeSinceStartup>shootTime) && !animPlaying)
            {
            if (ammo > 0)
            {
                ammo--;
                GetComponent<AudioSource>().PlayOneShot(gunSound[equipped]);
                shootTime = Time.realtimeSinceStartup + 0.2f;
                GetComponent<CapsuleCollider>().enabled = false;
                fireGun();
                GetComponent<CapsuleCollider>().enabled = true;
            }
            }
        anim.SetBool("Sprint", !partner);
        bool move = false;
        if (Time.realtimeSinceStartup > spinTime)
        {
            if (!partner)
            {
                move = moveOpp();
            }
        }
        if (move)
        {
            spinTime = Time.realtimeSinceStartup + 1;
        }
        GetComponent<CapsuleCollider>().center = new Vector3(0, 1, 0);
        GetComponent<CapsuleCollider>().radius = 1;
        GetComponent<CapsuleCollider>().height = 2;
        anim.SetBool("DoneSliding", !(rayDistances > 0.01f));
        sliding = false;
        
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Sprint"))
        {
            if (onGround && ((Input.GetKey(KeyCode.C))))
            {

                anim.Play("Slide");
            }
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
            {

                anim.Play(randomAnim(rlMoves));
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide") || anim.GetCurrentAnimatorStateInfo(0).IsName("SlideFlip") || anim.GetCurrentAnimatorStateInfo(0).IsName("SlideSprint"))
        {
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.Space))
            {

                anim.Play(randomAnim(rlMoves));
            }
            sliding = true;
            GetComponent<CapsuleCollider>().center = new Vector3(0, 0.5f, 0);
            GetComponent<CapsuleCollider>().radius = 0.5f;
            GetComponent<CapsuleCollider>().height = 0;
            vel = transform.forward * 10 * lockSpeed;
            if (rayDistances > 0.03f)
            vel += transform.forward * 15 * lockSpeed;
            vel.y = -900;
            return;
        }
            if (Physics.Raycast(transform.position+Vector3.up, Vector3.down, out h, 500, ground))
                transform.position = h.point;
        health.text = "H" + playerhealth + " MR " + moveRatio;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("ArmsCrouchRight") || anim.GetCurrentAnimatorStateInfo(0).IsName("ArmsCrouchLeft"))
        {
            anim.SetLayerWeight(2, 0.1f);

            anim.SetLayerWeight(1, 0.1f);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("CrouchFireLeft") || anim.GetCurrentAnimatorStateInfo(0).IsName("CrouchFireRight"))
        {
            anim.SetLayerWeight(2, 0.8f);

            anim.SetLayerWeight(1, 0.8f);
        }
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("DiveContd"))
        {
            GetComponent<Rigidbody>().velocity = diveDir * -10;
            return;
        }
    }
    bool moveOpp()
    {
        bool move = false;
        if (Input.GetKeyDown(KeyCode.A))
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
                anim.Play("SlideLeftRoll");
            else
                anim.Play(randomAnim(new string[] { "Spin", "SideFlipLeftAnim" }));
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Slide"))
                anim.Play("SlideRightRoll");
            else
                anim.Play(randomAnim(new string[] { "RightSpin", "SideFlipRightAnim" }));
            move = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {

            anim.Play("DiveContd");
            diveDir = transform.forward;
            move = true;
        }
        return move;
    }
    public void lockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void fireGun()
    {
        AnimatorStateInfo aInf = anim.GetCurrentAnimatorStateInfo(1);
        bool animPlaying = aInf.IsName("DualPistolsSprintFire") || aInf.IsName("DualPistolsFire") || aInf.IsName("StandStillShotgunFire") || aInf.IsName("FireShotgun") || aInf.IsName("StandStillUziFire") || aInf.IsName("FireUzi") || aInf.IsName("PistolFire");
        if (animPlaying)
            return;
        T1AT = Time.realtimeSinceStartup + 0.4f;

        if (equipped == 0)
        {
            anim.Play("PistolFire", 1);
            fireBullet();
        }
        if (equipped == 1)
        {
            if (!partner)
                anim.Play("FireUzi", 1);
            else
                anim.Play("StandStillUziFire", 1);
            fireBullet();
        }
        if (equipped == 2)
        {
            Vector3 startRot = transform.localEulerAngles;
            if (!partner)
                anim.Play("FireShotgun", 1);
            else
                anim.Play("StandStillShotgunFire", 1);
            for (int i = 0; i < 4; i++)
            {
                transform.Rotate(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
                fireBullet();
                transform.localEulerAngles = startRot;
            }

        }
        if (equipped == 3)
        {
            Vector3 startRot = transform.localEulerAngles;
            if (!partner)
                anim.Play("DualPistolsSprintFire", 1);
            else
                anim.Play("DualPistolsFire", 1);
            for (int i = 0; i < 2; i++)
            {
                transform.Rotate(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2));
                fireBullet();
                transform.localEulerAngles = startRot;
            }

        }
       
    }
    public void fireBullet()
    {
        
        RaycastHit lightPT;
        FindObjectOfType<PlayerAimCam>().recoil += 5;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out lightPT, gunLayers))
        {
            GameObject k = Instantiate(sound);
            k.transform.position = lightPT.point;
            GameObject k2 = Instantiate(bulletCol);
            k2.transform.position = lightPT.point;
            k2.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            k2.GetComponent<LineRenderer>().SetPosition(1, lightPT.point);
            k2.SetActive(true);
            k2.GetComponent<StoreObj>().obj.SetActive(true);
            Destroy(k2, 0.3f);

            k.transform.parent = null;
            k.GetComponent<SoundPoint>().Search();
            if (lightPT.transform.gameObject.GetComponent<ExplodingBarrel>())
            {
                GameObject k5 = Instantiate(lightPT.transform.gameObject.GetComponent<ExplodingBarrel>().explosion);
                k5.transform.position = lightPT.transform.gameObject.transform.position;
                if (Vector3.Distance(transform.position, lightPT.point) < 5)
                    FindObjectOfType<gameEnemy>().kill();
                Destroy(lightPT.transform.gameObject);
                Destroy(k5, 1);
            }
        }
        RaycastHit hit2;
        Debug.Log("FIRED");
            foreach (gameEnemy g in FindObjectsOfType<gameEnemy>())
            {
                Vector2 pos = Camera.main.WorldToScreenPoint(g.transform.position);
                Vector2 centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
                if (Vector2.Distance(pos, centerPoint) < Screen.width/ 51.2f)
                {
                Debug.Log("FOUND ENEMY" + g.name);
                if (Physics.Raycast(Camera.main.transform.position, Vector3.MoveTowards(g.transform.position-Camera.main.transform.position, new Vector3(), 1) ,out hit2, hitLayers) || Vector3.Distance(transform.position, g.transform.position) < 10)
                {
                    Debug.Log("HIT ENEMY " + hit2.collider.gameObject);
                    if (hit2.collider.GetComponent<Glass>())
                        hit2.collider.GetComponent<Glass>().BreakGlass();
                    if (hit2.transform.gameObject == g.gameObject)
                    {
                        hit(hit2);
                        return;
                    }
                }
                    
                }
            }
        foreach (MultiplayerCamera g in FindObjectsOfType<MultiplayerCamera>())
        {
            Vector2 pos = Camera.main.WorldToScreenPoint(g.transform.position);
            Vector2 centerPoint = new Vector2(Screen.width / 2, Screen.height / 2);
            if (Vector2.Distance(pos, centerPoint) < Screen.width / 51.2f)
            {
                Debug.Log("FOUND ENEMY" + g.name);
                if (Physics.Raycast(Camera.main.transform.position, Vector3.MoveTowards(g.transform.position - Camera.main.transform.position, new Vector3(), 1), out hit2, hitLayers) || Vector3.Distance(transform.position, g.transform.position) < 10)
                {
                    Debug.Log("HIT ENEMY " + hit2.collider.gameObject);
                    if (hit2.collider.GetComponent<Glass>())
                        hit2.collider.GetComponent<Glass>().BreakGlass();
                    if (hit2.transform.gameObject == g.gameObject)
                    {
                        hit(hit2);
                        return;
                    }
                }

            }
        }
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit2, hitLayers))
        {
            Debug.Log("HIT ENEMY " + hit2.collider.gameObject);
            if (hit2.collider.GetComponent<Glass>())
                hit2.collider.GetComponent<Glass>().BreakGlass();
            if (hit2.transform.gameObject.GetComponent<gameEnemy>())
            {
                hit(hit2);
                return;
            }
            if (hit2.transform.gameObject.GetComponent<MultiplayerCamera>())
            {
                hit(hit2);
                return;
            }
        }
        if (Physics.SphereCast(Camera.main.transform.position, 2, Camera.main.transform.forward, out hit2, hitLayers))
        {
            Debug.Log("HIT ENEMY " + hit2.collider.gameObject);
            if (hit2.transform.gameObject.GetComponent<gameEnemy>())
            {
                hit(hit2);
                return;
            }
            if (hit2.transform.gameObject.GetComponent<MultiplayerCamera>())
            {
                hit(hit2);
                return;
            }
        }

        
    }

    public void hit(RaycastHit hit2)
    {
        if (hit2.transform.gameObject.GetComponent<gameEnemy>())
        {
            hit2.transform.gameObject.layer = 9;
            Destroy(hit2.transform.gameObject.GetComponent<gameEnemy>());
            HitEnemy(hit2.transform.gameObject);
        }
        if (hit2.transform.gameObject.GetComponent<MultiplayerCamera>())
        {
            if (Time.realtimeSinceStartup < hit2.transform.gameObject.GetComponent<MultiplayerCamera>().killTime)
                return;
            hit2.transform.gameObject.GetComponent<MultiplayerCamera>().killTime = Time.realtimeSinceStartup + 5;
            HitPlayer(hit2.transform.gameObject);
        }
    }
}
