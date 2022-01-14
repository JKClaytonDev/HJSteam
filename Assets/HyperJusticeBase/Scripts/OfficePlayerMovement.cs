using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficePlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public Animator playerAnim;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float y = rb.velocity.y;
        rb.velocity = new Vector3(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"))*5;
        if (rb.velocity.x != 0 || rb.velocity.y != 0)
        {
            playerAnim.transform.LookAt(transform.position + rb.velocity);
            playerAnim.transform.Rotate(0, 0, 0);
        }
        rb.velocity+=Vector3.up*y;
    }
}
