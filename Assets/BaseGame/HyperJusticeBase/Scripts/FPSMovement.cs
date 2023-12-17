using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FPSMovement : MonoBehaviour
{
    float mouseSensValue;
    public GameObject cam;
    public LayerMask layers;
    float teleTime;
    // Start is called before the first frame update
    void Start()
    {
        mouseSensValue = PlayerPrefs.GetFloat("SENS");
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Physics.gravity = Vector3.up*-99;
    }

    // Update is called once per frame
    void Update()
    {
        cam.transform.localPosition = new Vector3(0, 2+(Mathf.Sin(transform.position.x/5) + Mathf.Cos(transform.position.z/5)), 0)/6;
        cam.transform.localEulerAngles = 4*new Vector3(Mathf.Sin(transform.position.x/8) + Mathf.Cos(transform.position.z / 8), 0, 0);
        transform.Rotate(0, Input.GetAxis("Mouse X") * (mouseSensValue / 50), 0);
        GetComponent<Rigidbody>().velocity = (transform.forward * Input.GetAxis("Vertical") * 5) + Vector3.up * GetComponent<Rigidbody>().velocity.y;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.gameObject.name.Contains("EndScene"))
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Time.realtimeSinceStartup > teleTime)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 6, layers))
            {
                if (hit.transform.gameObject.name.Contains("Teleport"))
                {
                    teleportAround a = FindObjectsOfType<teleportAround>()[Random.Range(0, FindObjectsOfType<teleportAround>().Length)];
                    
                        a = FindObjectsOfType<teleportAround>()[Random.Range(0, FindObjectsOfType<teleportAround>().Length)];
                    transform.position = a.transform.position - hit.transform.gameObject.transform.position + transform.position;
                    teleTime = Time.realtimeSinceStartup + 5;
                }

            }
        }
    }
}
