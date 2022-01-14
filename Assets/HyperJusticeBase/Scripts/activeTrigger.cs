using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activeTrigger : MonoBehaviour
{
    public GameObject obj;
    public float destroyTime;
    public string containsName = "DEFAULT";
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log("COLLIDED WITH " + other.name);
        if (containsName == "DEFAULT")
        {
            if (!other.gameObject.GetComponent<RobotController>())
                return;
        }
        else
        {
            if (!other.gameObject.name.Contains(containsName))
                return;
            obj.SetActive(false);
        }
        obj.transform.parent = null;
        obj.SetActive(!obj.activeInHierarchy);
        if (destroyTime != 0)
            Destroy(obj, destroyTime);
        Destroy(gameObject);
    }
}
