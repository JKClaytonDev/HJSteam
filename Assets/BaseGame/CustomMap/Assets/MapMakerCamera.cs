using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MapMakerCamera : MonoBehaviour
{
    public GameObject copied;
    public GameObject selectedObject;
    float camSpeed;
    bool inputMode;
    int blockMode = 1;
    Vector3 rot;
    bool objectDrag;
    Vector3 lastSelectedPos;
    // Start is called before the first frame update
    void Start()
    {
        camSpeed = 15;
    }

    // Update is called once per frame
    void Update()
    {
        if (selectedObject && selectedObject.GetComponent<CantSelect>())
            selectedObject = null;
        if (Input.GetMouseButton(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(r, out mouseHit) && selectedObject)
            {
                if (mouseHit.transform.gameObject == selectedObject || objectDrag)
                {
                    objectDrag = true;
                    Vector3 newPos = selectedObject.transform.position;
                    newPos.z = mouseHit.point.z;
                    newPos.x = mouseHit.point.x;
                    
                    if (lastSelectedPos != new Vector3())
                    selectedObject.transform.position += newPos - lastSelectedPos;
                    lastSelectedPos = newPos;
                    return;
                }
            }

        }
        else
        {
            lastSelectedPos = new Vector3();
            objectDrag = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        if (Input.GetKeyUp(KeyCode.LeftControl))
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        if (Time.realtimeSinceStartup < 1 )
        {
            selectedObject = null;
            return;
        }
        UnityEngine.Cursor.visible = true;
        //Input
        //Universal Keys
        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.C))
        {
            copyBlock();
            Debug.Log("COPY");
        }
        else if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V))
        {
            Debug.Log("PASTE");
            pasteBlock(copied);
            
        }
        else if (Input.GetKeyDown(KeyCode.Delete) || Input.GetKeyDown(KeyCode.Backspace))
        {
            breakBlock();
        }
        else if (Input.GetKeyDown(KeyCode.C))
            inputMode = !inputMode;
        else if (Input.GetKeyDown(KeyCode.O))
            GetComponent<Camera>().orthographic = !GetComponent<Camera>().orthographic;
        //Input modes
        if (inputMode)
        {
            Vector3 move = new Vector3(Time.deltaTime / Time.timeScale * 250 * -Input.GetAxis("Mouse Y"), Time.deltaTime / Time.timeScale * 250 * Input.GetAxis("Mouse X"), 0);
            transform.position += 15 * camSpeed * Time.deltaTime / Time.timeScale * (Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right);
            if (Input.GetKey(KeyCode.F))
            {
                if (Mathf.Abs(move.x) > Mathf.Abs(move.y) && Mathf.Abs(move.x) > Mathf.Abs(move.z))
                {
                    move.y = 0;
                    move.z = 0;
                }
                else if (Mathf.Abs(move.y) > Mathf.Abs(move.z) && Mathf.Abs(move.y) > Mathf.Abs(move.x))
                {
                    move.x = 0;
                    move.z = 0;
                }
                else if (Mathf.Abs(move.z) > Mathf.Abs(move.y) && Mathf.Abs(move.z) > Mathf.Abs(move.x))
                {
                    move.x = 0;
                    move.y = 0;
                }
            }
            transform.localEulerAngles += move;
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            UnityEngine.Cursor.lockState = CursorLockMode.None;
        }

        //Selecting Blocks
        if (Input.GetMouseButtonDown(0)){
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit))
            {
                selectedObject = hit.collider.gameObject;
                while (selectedObject.transform.parent != null)
                    selectedObject = selectedObject.transform.parent.gameObject;
            }
        }

        Vector3 dirr = transform.localEulerAngles;
        Vector3 newDir = dirr;
        newDir.x = 0;
        newDir.z = 0;
        transform.localEulerAngles = newDir;

        //Moving Blocks
        if (Input.GetKey("1"))
            setBlockMode(1);
        if (Input.GetKey("2"))
            setBlockMode(2);
        if (Input.GetKey("3"))
            setBlockMode(3);
        else
        if (selectedObject && !inputMode)
        {


            Vector3 dir = new Vector3();
            if (blockMode == 3)
                transform.Rotate(0, 90, 0);
            if (Input.GetKey(KeyCode.D) && blockMode != 2 || blockMode == 2 && Input.GetKey(KeyCode.Q))
                dir += transform.right;
            if (Input.GetKey(KeyCode.A) && blockMode != 2 || blockMode == 2 && Input.GetKey(KeyCode.E))
                dir -= transform.right;
            if (Input.GetKey(KeyCode.Q) && blockMode != 2 || blockMode == 2 && Input.GetKey(KeyCode.A))
                dir += transform.up;
            if (Input.GetKey(KeyCode.E) && blockMode != 2 || blockMode == 2 && Input.GetKey(KeyCode.D))
                dir -= transform.up;
            if (Input.GetKey(KeyCode.W))
                dir += transform.forward;
            if (Input.GetKey(KeyCode.S))
                dir -= transform.forward;
            if (Input.GetKey(KeyCode.LeftShift))
                dir /= 2;
            if (blockMode == 3)
                transform.Rotate(0, -90, 0);
            if (!Input.GetKey(KeyCode.F))
            {
                if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y) && Mathf.Abs(dir.x) > Mathf.Abs(dir.z))
                {
                    dir.y = 0;
                    dir.z = 0;
                }
                else if (Mathf.Abs(dir.y) > Mathf.Abs(dir.z) && Mathf.Abs(dir.y) > Mathf.Abs(dir.x))
                {
                    dir.x = 0;
                    dir.z = 0;
                }
                else if (Mathf.Abs(dir.z) > Mathf.Abs(dir.y) && Mathf.Abs(dir.z) > Mathf.Abs(dir.x))
                {
                    dir.x = 0;
                    dir.y = 0;
                }
            }
            if (Input.GetKey(KeyCode.LeftControl))
            {
                dir += transform.forward * Input.GetAxis("Mouse Y") + transform.right * Input.GetAxis("Mouse X");
            }
            dir *= 350 * Time.deltaTime;
            if (blockMode == 1)
            {
                selectedObject.transform.position += dir / 5;
            }
            else
            {
                if (blockMode == 2)
                    selectedObject.transform.localEulerAngles += dir / 1;
                else if (blockMode == 3)
                    selectedObject.transform.localScale += dir / 5;
            }

        }

        transform.localEulerAngles = dirr;
    }

    public void pasteBlock(GameObject block)
    {
        GameObject g = Instantiate(block);
        g.transform.parent = null;
        selectedObject = g;
    }
    public void pasteBlock(int block)
    {
        GameObject g = Instantiate(FindObjectOfType<CustomMapMaker>().prefabs[block]);
        g.transform.parent = null;
        selectedObject = g;
    }
    public void placeBlock(int block)
    {
        GameObject g = Instantiate(FindObjectOfType<CustomMapMaker>().prefabs[block]);
        g.transform.parent = null;
        Vector3 pos = transform.position + transform.forward * 55;
        g.transform.position = pos;
        selectedObject = g;
    }
    public void placeBlock(GameObject block)
    {
        GameObject g = Instantiate(block);
        g.transform.parent = null;
        Vector3 pos = transform.position + transform.forward * 55;
        g.transform.position = pos;
        selectedObject = g;
    }
    public void copyBlock()
    {
        if (selectedObject)
            copied = selectedObject;
    }
    public void setBlockMode(int mode) {
        blockMode = mode;
    }
    public void cloneBlock()
    {
        if (selectedObject)
        {
            GameObject g = Instantiate(selectedObject);
            g.transform.parent = null;
            selectedObject = g;
        }
    }
    public void breakBlock()
    {
        if (selectedObject && FindObjectsOfType<CubeMover>().Length >= 1)
        {
            Destroy(selectedObject);
        }
    }
}
