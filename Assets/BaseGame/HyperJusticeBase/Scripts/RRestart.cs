using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RRestart : MonoBehaviour
{
    private void Start()
    {
        foreach (gameEnemy E in FindObjectsOfType<gameEnemy>())
        {
            E.enabled = false;
        }
        foreach (propSound E in FindObjectsOfType<propSound>())
        {
            E.enabled = false;
        }
    }
    void Update()
    {
        if (FindObjectOfType<stoptesting>())
        {
            FindObjectOfType<stoptesting>().deactivate();
            Destroy(transform.parent.parent.gameObject);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.localScale *= 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            this.enabled = false;
        }
    }
}
