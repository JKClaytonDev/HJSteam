using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public float speed;
    float throwTime;
    public GameObject explosion;
    RobotController player;
    // Start is called before the first frame update
    void Start()
    {
        throwTime = Time.realtimeSinceStartup + 1.5f;
        GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * speed;
        player = FindObjectOfType<RobotController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.realtimeSinceStartup < player.fireTime + 0.1f)
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Debug.Log("POS" + pos);
            if (pos.x - (Screen.width / 2) < Screen.width / 60 && pos.y - (Screen.height / 2) < Screen.height / 20)
            {
                explode();
            }
        }
        if (Time.realtimeSinceStartup > throwTime)
        {
            explode();
        }
    }
    void explode()
    {
        GameObject f = Instantiate(explosion);
        f.transform.position = transform.position;
        Destroy(f, 2);
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 8);
        GameObject k = Instantiate(player.sound);
        k.transform.position = transform.position;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.GetComponent<gameEnemy>())
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, Vector3.MoveTowards(transform.position, hitCollider.transform.position, 1) - transform.position, out hit);
                if (hit.transform.gameObject.GetComponent<gameEnemy>())
                    FindObjectOfType<RobotController>().HitEnemy(hitCollider.gameObject);
            }
        }
        Destroy(gameObject);
    }
}
