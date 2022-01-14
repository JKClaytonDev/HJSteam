using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class unlockmouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string s = SceneManager.GetActiveScene().name;
        if (s.ToCharArray()[0] == 'W' && s.Contains("World"))
        {
            for (int i = 0; i < 13; i++)
            {
                PlayerPrefs.SetInt(i + "", 0);
            }
        }
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
