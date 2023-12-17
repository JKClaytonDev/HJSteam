using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class stoptesting : MonoBehaviour
{
    public GameObject player;
    public GameObject[] activate;
    public bool[] active;
    public bool toggled;
    GameObject p;
    // Start is called before the first frame update
    public void activateObject() {
        p = Instantiate(player);
        p.SetActive(true);
    }
    public void deactivateObject()
    {
        Destroy(p);
        foreach (RobotController t in FindObjectsOfType<RobotController>())
            Destroy(t);
        foreach (StoreRBs t in FindObjectsOfType<StoreRBs>())
            Destroy(t.gameObject);
        foreach (gameEnemy h in FindObjectsOfType<gameEnemy>())
            Destroy(h);
        
        foreach (enemySpawner t in FindObjectsOfType<enemySpawner>())
        {
            t.GetComponent<MeshRenderer>().enabled = true;
            t.GetComponent<SphereCollider>().enabled = true;
        }
        Debug.Log("DEACTIVATED");
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
     if (Input.GetKeyDown(KeyCode.T))
        {

                deactivate();
        }   
    }
    public void deactivate()
    {
        for (int i = 0; i < activate.Length; i++)
        {
            deactivateObject();
            activate[i].SetActive(active[i]);
        }
    }
}
