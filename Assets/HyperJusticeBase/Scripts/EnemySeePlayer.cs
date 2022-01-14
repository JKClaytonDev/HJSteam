using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySeePlayer : MonoBehaviour
{
    public float scale;
    public GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        scale = 0;
        foreach (gameEnemy g in FindObjectsOfType<gameEnemy>()) {
            if (g.onLine)
                scale+=Mathf.Min(0.5f, Mathf.Max(2, 3/Vector3.Distance(FindObjectOfType<RobotController>().transform.position, g.transform.position)));
                    }
        transform.localScale = new Vector3(scale, 3, 1);

        }

}
