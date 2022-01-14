using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public void Search()
    {
        float dist = 0;
        foreach (gameEnemy g in FindObjectsOfType<gameEnemy>())
        {
            dist = Vector3.Distance(transform.position, g.transform.position);
            if (dist < 50)
            {
                RaycastHit hit;
                Physics.Raycast(g.transform.position, new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), out hit);
                g.targetDest = Vector3.MoveTowards(transform.position, hit.point, dist);
                g.speed = 30;
            }
            if (dist < 10)
            {
                RaycastHit hit;
                Physics.Raycast(transform.position, new Vector3(Random.Range(-1, 1), 0, Random.Range(-1, 1)), out hit);
                g.targetDest = Vector3.MoveTowards(transform.position, hit.point, dist);
                g.speed = 50;
            }
        }
        Destroy(gameObject, 1);
    }
}
