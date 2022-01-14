using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class hideAfterLevel : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex > index)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
