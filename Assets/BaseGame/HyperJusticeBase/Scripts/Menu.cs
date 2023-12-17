using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    private void Start()
    {
        if (!PlayerPrefs.HasKey("VOL"))
        {
            PlayerPrefs.SetFloat("VOL", 1);
            QualitySettings.SetQualityLevel(2);
        }
        if (!PlayerPrefs.HasKey("SENS"))
            PlayerPrefs.SetFloat("SENS", 150 / 3);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void newGame()
    {
        SceneManager.LoadScene("Story0");
    }
    public void Settings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void continueGame()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("CurrentScene"));
    }
    public void Quit()
    {
        Application.Quit();
    }
}
