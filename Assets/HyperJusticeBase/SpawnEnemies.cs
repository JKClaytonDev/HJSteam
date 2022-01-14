using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        foreach (enemySpawner t in FindObjectsOfType<enemySpawner>())
        {
            t.GetComponent<MeshRenderer>().enabled = false;
            t.GetComponent<SphereCollider>().enabled = false;
            GameObject k = Instantiate(enemy);
            k.transform.position = t.transform.position;
        }
    }

}
