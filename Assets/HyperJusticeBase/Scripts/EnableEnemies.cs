using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableEnemies : MonoBehaviour
{
    public GameObject[] prompts;
    public GameObject[] enemies;
    public GameObject tempEnemy;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject p in prompts)
        {
            if (p.activeInHierarchy)
                return;
        }
        foreach (GameObject p in enemies)
            p.SetActive(true);
        Destroy(tempEnemy);
    }
}
