using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeChildren : MonoBehaviour
{
    public bool wait = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        if (wait)
            return;
        detach();
    }

    // Update is called once per frame
    private void OnEnable()
    {
        if (wait)
            return;
        detach();
    }
    public void detach()
    {
        foreach (Transform g in transform)
            g.transform.parent = null;
        if (wait)
            Destroy(gameObject);
    }
}
