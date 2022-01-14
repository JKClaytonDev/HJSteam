using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerCombo : MonoBehaviour
{
    public RobotController movement;
    float lastCombo;
    Vector3 scale = new Vector3(0.2f, 0.5f, 1);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale *= 1 - (0.3f*Time.deltaTime);
        if (movement.combo != lastCombo || lastCombo == 0)
            transform.localScale = scale;
        lastCombo = movement.combo;
        if (transform.localScale.x < 0.1f)
            movement.combo = 0;

    }
}
