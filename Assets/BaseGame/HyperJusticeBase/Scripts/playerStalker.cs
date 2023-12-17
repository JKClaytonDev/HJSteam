using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class playerStalker : MonoBehaviour
{
    public float custSpeed = 1;
    
    float moveTime;
    public GameObject player;
    StalkerSpawn[] spawns;
    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(640, 480, true);
        if (FindObjectOfType<StalkerSpawn>())
        {
            spawns = FindObjectsOfType<StalkerSpawn>();
            GameObject pickSpawn = spawns[Random.Range(0, spawns.Length - 1)].gameObject;
            GetComponent<NavMeshAgent>().nextPosition = pickSpawn.transform.position;
        }
        GetComponent<NavMeshAgent>().angularSpeed*=custSpeed;
        GetComponent<NavMeshAgent>().speed = 0.5f;
        if (!player)
        player = FindObjectOfType<FPSMovement>().gameObject;
        GetComponent<NavMeshAgent>().destination = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<NavMeshAgent>().speed = (Mathf.Sin(Time.realtimeSinceStartup / 5) + 2)*custSpeed;
        if (FindObjectOfType<StalkerSpawn>())
        {
            if (Vector3.Distance(transform.position, GetComponent<NavMeshAgent>().destination) < 2)
            {
                GameObject pickSpawn = spawns[Random.Range(0, spawns.Length - 1)].gameObject;
                GetComponent<NavMeshAgent>().destination = pickSpawn.transform.position;
            }
        }
        RaycastHit h;
        if (Physics.Raycast(transform.position, Vector3.MoveTowards(new Vector3(), player.transform.position-transform.position, 1), out h)){
            if (h.transform.gameObject == player)
            {
                GetComponent<NavMeshAgent>().destination = player.transform.position;
            }
            else if (FindObjectOfType<teleportAround>() && Time.realtimeSinceStartup > moveTime)
            {
                moveTime = Time.realtimeSinceStartup + 5;
                GetComponent<NavMeshAgent>().nextPosition = FindObjectsOfType<teleportAround>()[Random.Range(0, FindObjectsOfType<teleportAround>().Length)].gameObject.transform.position;
                transform.position = GetComponent<NavMeshAgent>().nextPosition;
            }
        }
        float distance = Vector3.Distance(transform.position, player.transform.position);
        GetComponent<Animator>().SetFloat("Distance", distance);
        if (distance < 2)
        {
            if (player.GetComponent<FPSMovement>())
            player.GetComponent<FPSMovement>().enabled = false;
            player.transform.LookAt(transform);
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
