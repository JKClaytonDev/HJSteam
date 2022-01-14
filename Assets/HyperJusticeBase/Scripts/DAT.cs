using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAT : MonoBehaviour
{
    public float timeTill;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeTill); 
    }

}
