using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class gameEnemy : MonoBehaviour
{
    bool chase;
    public bool Melee;
    public bool unlocked;
    public AudioClip fire;
    float fireSound;
    public Vector3 targetDest;
    float checkTime = 50;
    bool lastTrue;
    public float speed = 5;
    public GameObject player;
    float shootTime;
    float lineEnabled;
    Vector3 storePos;
    public float randWalk;
    public AudioClip step;
    Vector3 lastPos;
    public GameObject gun;
    public string[] animNames;
    public AudioClip missSound;
    float animTime;
    string UsingGun;
    public GameObject textObj;
    Vector3 startPos;
    public bool found;
    string[] guns = { "Uzi", "Shotgun", "Deagle" };
    public AudioClip[] voiceLines;
    public GameObject sphere;
    public bool onLine;
    float sphereTime;
    float Difficulty;
    bool rtk;
    float checkFireTime;
    NavMeshAgent mesh;
    RobotController playerRobotController;
    headPos h;
    // Start is called before the first frame update
    void Start()
    {
        if (!PlayerPrefs.HasKey("Difficulty"))
            PlayerPrefs.SetFloat("Difficulty", 1);
        mesh = GetComponent<NavMeshAgent>();
        found = false;
        startPos = transform.position;
        GetComponent<NavMeshAgent>().enabled = false;
        UsingGun = guns[Random.Range(0, guns.Length)];
        GetComponent<Animator>().Play("EnemyHold" + UsingGun, 1);
        GetComponent<NavMeshAgent>().speed = 0;
        randWalk = Random.Range(0.03f, 0.1f);
        checkTime = Random.Range(0.1f, 1);
        RaycastHit hit;
        Physics.Raycast(transform.position, new Vector3(Random.Range(-1, 1), -2, Random.Range(-1, 1)), out hit);
        targetDest = Vector3.MoveTowards(transform.position, hit.point, Random.Range(1, hit.distance / 2));
        speed = Random.Range(1, 3);
        player = FindObjectOfType<RobotController>().gameObject;
        textObj.SetActive(false);
        transform.Rotate(0, Random.Range(0, 180), 0);
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            GetComponent<NavMeshAgent>().enabled = false;
            GetComponent<NavMeshAgent>().enabled = true;
            GetComponent<NavMeshAgent>().SetDestination(hit.point);
        }
        Difficulty = PlayerPrefs.GetFloat("Difficulty");
        h = FindObjectOfType<headPos>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Time.frameCount % 2 == 0)
            return;
        if (!player)
            player = FindObjectOfType<RobotController>().gameObject;
        if (!playerRobotController)
            playerRobotController = FindObjectOfType<RobotController>();
        sphere.SetActive(Time.realtimeSinceStartup < sphereTime);

        if (transform.position.y < startPos.y - 100)
            Destroy(gameObject);

        if (!found) {
            RaycastHit hit3;
            Physics.Raycast(gun.transform.position, Vector3.MoveTowards(gun.transform.position, player.transform.position, 1) - gun.transform.position, out hit3);
            if (hit3.transform.gameObject.GetComponent<RobotController>())
            {
                mesh.SetDestination(playerRobotController.transform.position);
                float vol = Vector3.Distance(transform.position, player.transform.position);
                vol = 10 / vol;
                vol = Mathf.Min(1f, Mathf.Max(0.3f, vol));
                playerRobotController.GetComponent<AudioSource>().PlayOneShot(voiceLines[Random.Range(0, voiceLines.Length)], vol);
                Debug.Log("VOICE LINE");
                mesh.speed = 15 * Difficulty;
                found = true;
            }
            transform.position = startPos;
            return;
        }

        onLine = false;
        if (found) {
            onLine = true;
            mesh.SetDestination(playerRobotController.transform.position);
        }
        mesh.speed = speed * 3 * Difficulty;
        mesh.SetDestination(targetDest);


        if (Time.realtimeSinceStartup > animTime)
        {
            GetComponent<Animator>().Play(animNames[Random.Range(0, animNames.Length)]);
            animTime = Time.realtimeSinceStartup + Random.Range(1, 5);
        }
        if (Vector3.Distance(gun.transform.position, player.transform.position) < 5 && player.GetComponent<RobotController>().moveRatio == 1)
            player.GetComponent<RobotController>().playerhealth -= 15 / Difficulty;
        if (Vector3.Distance(gun.transform.position, lastPos) > 2)
        {
            GetComponent<echo>().playSound(step);
            lastPos = transform.position;
        }
        if (Time.realtimeSinceStartup < shootTime)
            return;
        if (Time.realtimeSinceStartup > checkFireTime)
        {
            Debug.Log("VALUE IS " + (0.5f / Difficulty) / Mathf.Sqrt(SceneManager.GetActiveScene().buildIndex));
            checkFireTime = Time.realtimeSinceStartup + (0.5f/Difficulty)/Mathf.Sqrt(SceneManager.GetActiveScene().buildIndex);
            if (Time.realtimeSinceStartup > lineEnabled + (player.GetComponent<RobotController>().dodgeMeter.transform.localScale.x + 0.2f) && !rtk)
            {
                rtk = true;
                Debug.Log("LINE ENABLED KILLING NOW");
                sphereTime = Time.realtimeSinceStartup + 0.1f;
                GetComponent<AudioSource>().PlayOneShot(fire);
            }
        }
        if (rtk && Time.realtimeSinceStartup > lineEnabled + (player.GetComponent<RobotController>().dodgeMeter.transform.localScale.x + 0.3f))
        {
            rtk = false;
        }
        else if (lastTrue)
        {
            RaycastHit hit;
            Physics.Raycast(gun.transform.position, Vector3.MoveTowards(gun.transform.position, player.transform.position, 1) - gun.transform.position, out hit);
            bool test = hit.transform.gameObject.GetComponent<RobotController>();
            GetComponent<LineRenderer>().SetPosition(0, gun.transform.position);
            if (Time.realtimeSinceStartup > lineEnabled + 2)
            {
                lineEnabled = Time.realtimeSinceStartup;
                Debug.Log("LINE ENABLED");
            }
            GetComponent<LineRenderer>().SetPosition(1, hit.point);
            if (test)
            {

                transform.LookAt(player.transform);
                Vector3 rott = transform.localEulerAngles;
                rott.x = 0;
                rott.z = 0;
                transform.localEulerAngles = rott;
                mesh.speed = speed * 3;
                if (GetComponent<LineRenderer>() && h)
                    GetComponent<LineRenderer>().SetPosition(1, h.transform.position);
            }
        }
        if (Time.realtimeSinceStartup > checkTime)
        {
            Debug.Log("RUNNING");
            checkTime = Time.realtimeSinceStartup + Random.Range(0.5f, 0.4f);
            RaycastHit hit;
            Physics.Raycast(gun.transform.position, Vector3.MoveTowards(gun.transform.position, player.transform.position, 1) - gun.transform.position, out hit);
            bool test = hit.transform.gameObject.GetComponent<RobotController>();
            if (test)
            {
                transform.LookAt(player.transform);
                Vector3 rott = transform.localEulerAngles;
                rott.x = 0;
                rott.z = 0;
                transform.localEulerAngles = rott;
                checkTime = Time.realtimeSinceStartup + (Vector3.Distance(transform.position, player.transform.position)/25)/Difficulty;
            }
            Debug.Log("HIT " + test);
            transform.LookAt(hit.point);
            mesh.SetDestination(targetDest);
            if (test && test == lastTrue)
            {
                
                shootTime = Time.realtimeSinceStartup + 0.2f/Difficulty;
                storePos = player.transform.position;
            }
            lastTrue = test;
        }
        
        
        if (Time.realtimeSinceStartup > shootTime)
        {
            RaycastHit hit;
            Physics.Raycast(gun.transform.position, Vector3.MoveTowards(gun.transform.position, player.transform.position, 1) - gun.transform.position, out hit);
            bool test = hit.transform.gameObject.GetComponent<RobotController>();
            if (hit.collider.GetComponent<Glass>())
            {
                Debug.Log("ENEMY BROKE");
                hit.collider.GetComponent<Glass>().BreakGlass();

            }
            if (!test)
            {
                return;
            }
            if (Vector3.Distance(player.transform.position, storePos) < 18/Difficulty)
            {
                textObj.SetActive(true);
                if (Time.realtimeSinceStartup > fireSound)
                {
                    Debug.Log("PLAYED FIRE SOUND");
                    sphereTime = Time.realtimeSinceStartup + 0.1f;
                    GetComponent<echo>().playSound(fire, 6);
                    kill();
                    fireSound = Time.realtimeSinceStartup + Random.Range(0.1f, 1);
                }
                if (Time.realtimeSinceStartup < player.GetComponent<RobotController>().hitTime)
                    return;
                shootTime = 0;
                player.GetComponent<RobotController>().hitTime = Time.realtimeSinceStartup + ((player.GetComponent<RobotController>().moveRatio) / 6)/(Difficulty*Difficulty);
                
                
            }
        }
        
        transform.LookAt(player.transform);


    }
    public void kill()
    {
        player.GetComponent<RobotController>().playerhealth -= Difficulty * (225) / (player.GetComponent<RobotController>().moveRatio + 0.01f);
        if (player.GetComponent<RobotController>().playerhealth < 0)
        {
            sphereTime = Time.realtimeSinceStartup + 0.3f;
            foreach (enemyText t in FindObjectsOfType<enemyText>())
                Destroy(t.gameObject);
            AudioClip sound = player.GetComponent<RobotController>().dieSound;
            GetComponent<Animator>().speed = 0;
            GameObject k = Instantiate(player.GetComponent<RobotController>().dead);
            k.transform.position = player.transform.position;
            k.transform.LookAt(transform);
            k.transform.parent = null;
            k.GetComponent<Rigidbody>().velocity = Vector3.up;
            Destroy(player);
            Time.timeScale = 1;
            k.GetComponent<AudioSource>().PlayOneShot(sound);
            this.enabled = false;

        }
    }
}
