using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorHitEnemy : MonoBehaviour
{
    public GameObject inst;
    void Start()
    {
        
    }
    void OnCollisionStay(Collision collision)
    {
        if (!this.enabled)
            return;
        if (collision.gameObject.layer == 10)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            FindObjectOfType<RobotController>().HitEnemy(collision.gameObject);
            GameObject k = Instantiate(inst);
            k.transform.position = transform.position;
            k.transform.localEulerAngles = transform.localEulerAngles;
            name = "BROKENDOOR";
            transform.position -= Vector3.up * 500;
            foreach (Rigidbody r in k.GetComponent<StoreRBs>().list)
            {
                r.gameObject.transform.parent = null;
            }
            
            Destroy(gameObject);
        }
    }
}
