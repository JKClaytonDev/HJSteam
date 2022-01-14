using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SettingsScreen : MonoBehaviour
{
    float resTime;
    bool changeRes;
    public void setGraphics(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }
    public void toggle96()
    {
        changeRes = true;
        if (Screen.width != 640)
            Screen.SetResolution(640, 360, false);
        else
            Screen.SetResolution(Display.main.systemWidth, Display.main.systemHeight, false);


            Screen.fullScreen = false;
            resTime = Time.realtimeSinceStartup + 0.1f;

    }
    public void toggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }
    public void setSens(float s)
    {
        PlayerPrefs.SetFloat("Sens", s);
    }
    public void returnMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
            if (changeRes && Time.realtimeSinceStartup > resTime)
            {
            changeRes = false;
            Screen.fullScreen = true;
            }
    }
}
