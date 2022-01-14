using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clonePoints : MonoBehaviour
{
    public GameObject parent;
    public GameObject child;
    public List<GameObject> temp;
    public GameObject[] listOfChildren;
    public GameObject[]  listOfPlayerChildren;
    private void GetChildRecursive(GameObject obj)
    {

        if (null == obj)
            return;
        if (obj.gameObject.GetComponent<NotInClone>())
            return;
        foreach (Transform child in obj.transform)
        {
            if (null == child)
                continue;
            //child.gameobject contains the current child you can do whatever you want like add it to an array
            if (!obj.gameObject.GetComponent<NotInClone>())
                temp.Add(child.gameObject);
            GetChildRecursive(child.gameObject);
        }
    }
        // Start is called before the first frame update
        void Start()
        {
        temp = new List<GameObject>();
        GetChildRecursive(child);
        listOfChildren = temp.ToArray();
        temp = new List<GameObject>();
        GetChildRecursive(parent);
        listOfPlayerChildren = temp.ToArray(); 
        }

    // Update is called once per frame
    public void clone()
    {
        return;
        {
            for (int i = 0; i<listOfChildren.Length; i++)
            {
                listOfChildren[i].transform.localPosition = listOfPlayerChildren[i].transform.localPosition;
                listOfChildren[i].transform.localRotation = listOfPlayerChildren[i].transform.localRotation;
            }
        }
    }
}
