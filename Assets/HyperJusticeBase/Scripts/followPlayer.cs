using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class followPlayer : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
        {
             player = FindObjectOfType<RobotController>().gameObject;
            if (!player)
            return;
            transform.position = player.transform.position;
        }
        if (Vector3.Distance(transform.position, player.transform.position) > 5)
        GetComponent<NavMeshAgent>().destination = player.transform.position;
    }
}
