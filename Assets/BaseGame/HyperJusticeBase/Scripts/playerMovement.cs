using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class playerMovement : MonoBehaviour
{
    //playerlook
    public GameObject head;

    //bhopmovement
    public bool onGround = false;
    public float accel = 200;
    public float airAccel = 200;
    public float maxSpeed = 10;
    public float maxAirSpeed = 0.6f;
    public float friction = 8;
    public LayerMask groundLayers;
    float jumpForce = 5;
    float jumpPressTime = -1;
    float jumpPressDuration = 0.1f;
    public float speedMultiplier;
    int groundFrames = 0;
    Vector3 fwVel;
    //others
    float vertInput;
    public int autoHop;
    public float height;
    public Rigidbody rb;
    Vector3 lastPos;
    Animator anim;
    float mouseSensValue;
    // Start is called before the first frame update
    void Start()
    {
        mouseSensValue = PlayerPrefs.GetFloat("SENS");
        anim = GetComponent<Animator>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            anim.speed = 1.1f;
            onGround = CheckonGround();
            anim.SetBool("OnGround", onGround);
            transform.localEulerAngles += new Vector3(0, Input.GetAxis("Mouse X") * (mouseSensValue / 50), 0);
            head.transform.localEulerAngles += new Vector3(-Input.GetAxis("Mouse Y") * (mouseSensValue / 50), 0, 0);
            if (Input.GetKey(KeyCode.Space) || autoHop == 1)
                jumpPressTime = Time.time;
        }
        lastPos = transform.position;

    }
    private void FixedUpdate()
    {

        GetComponent<Rigidbody>().velocity /= speedMultiplier;
        GetComponent<Rigidbody>().velocity -= fwVel;
        if (onGround)
        {
            groundFrames++;
            if (groundFrames > 10)
                vertInput = Input.GetAxis("Vertical");
        }
        else
        {
            groundFrames = 0;
        }
        fwVel = transform.forward * Time.deltaTime * vertInput * 371;
        Vector2 input = new Vector2(Input.GetAxis("Horizontal") + Input.GetAxis("Mouse X") * (mouseSensValue / 50) * autoHop, Input.GetAxis("Vertical") * autoHop);
        input.x = Mathf.Min(Mathf.Max(-1, input.x), 1);
        Vector3 playerVel = rb.velocity;
        playerVel = Friction(playerVel);
        playerVel += calculateMovement(input, playerVel);
        rb.velocity = playerVel;
        rb.velocity += fwVel;
        rb.velocity *= speedMultiplier;
    }
    Vector3 Friction(Vector3 currentVel)
    {
        float playerSpeed = currentVel.magnitude;

        if (!onGround || Input.GetKey(KeyCode.Space) || autoHop == 1 || playerSpeed == 0)
            return currentVel;

        float drop = playerSpeed * friction * Time.deltaTime;
        return currentVel * Mathf.Max(playerSpeed - drop, 0) / playerSpeed;
    }
    Vector3 calculateMovement(Vector2 input, Vector3 velocity)
    {
        float curAccel = accel;
        if (!onGround)
            curAccel = airAccel;
        float curMaxSpeed = maxSpeed;
        if (!onGround)
            curMaxSpeed = maxAirSpeed;
        Vector3 camRotation = new Vector3(0f, head.transform.rotation.eulerAngles.y, 0f);
        Vector3 inputVelocity = Quaternion.Euler(camRotation) *
                                new Vector3(input.x * curAccel, 0f, input.y * curAccel);
        Vector3 alignedInputVelocity = new Vector3(inputVelocity.x, 0f, inputVelocity.z) * Time.deltaTime;
        Vector3 currentVelocity = new Vector3(velocity.x, 0f, velocity.z);
        float max = Mathf.Max(0f, 1 - (currentVelocity.magnitude / curMaxSpeed));
        float velocityDot = Vector3.Dot(currentVelocity, alignedInputVelocity);
        Vector3 modifiedVelocity = alignedInputVelocity * max;
        Vector3 correctVelocity = Vector3.Lerp(alignedInputVelocity, modifiedVelocity, velocityDot);
        correctVelocity += GetJumpVelocity(velocity.y);
        return correctVelocity;
    }
    Vector3 GetJumpVelocity(float yVelocity)
    {
        Vector3 jumpVelocity = Vector3.zero;

        if (Time.time < jumpPressTime + jumpPressDuration && yVelocity < jumpForce && CheckonGround())
        {
            jumpPressTime = -1f;
            if (Input.GetKey(KeyCode.F))
                jumpVelocity = new Vector3(0f, (5*jumpForce) - yVelocity, 0f);
            else
            jumpVelocity = new Vector3(0f, jumpForce - yVelocity, 0f);
        }

        return jumpVelocity;
    }
    bool CheckonGround()
    {
        return (Physics.Raycast(transform.position+Vector3.up, Vector3.down, height, groundLayers));
    }
}
