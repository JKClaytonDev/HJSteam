using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingsMenu : MonoBehaviour
{
    
    // Update is called once per frame
    void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }
    public void loadLevel(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void setPartner(int i)
    {
        PlayerPrefs.SetInt("Partner", i);
        if (i == -1)
            PlayerPrefs.DeleteKey("Partner");
    }
    public void setDifficulty(int f)
    {
        float d = (float)f;
        Debug.Log("DIFF DIFF DIFF DIFF");
        PlayerPrefs.SetFloat("Difficulty", d/3);
        
        
        Debug.Log("SAVED DIFFICULTY TO " + PlayerPrefs.GetFloat("Difficulty"));
        PlayerPrefs.Save();
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
