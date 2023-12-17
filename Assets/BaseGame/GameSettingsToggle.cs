using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsToggle : MonoBehaviour
{
    public RobotController player;
    public GameObject settings;
    float settingsFalseTime;
    float settingsCloseTime;
    float checkTime;
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        if (!player)
        {
            player = FindObjectOfType<RobotController>();
            player.settings = settings;

        }

        if (settings.activeInHierarchy)
        {
            if (!player)
                player = FindObjectOfType<RobotController>();
            player.enabled = false;
            Cursor.lockState = CursorLockMode.None;
            if (Input.GetKeyUp(KeyCode.Escape))
                Cursor.visible = true;
            if (Input.GetKeyDown(KeyCode.Escape) && Time.realtimeSinceStartup > settingsFalseTime + 0.2f)
            {
                settings.SetActive(false);
                player.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                settingsCloseTime = Time.realtimeSinceStartup;
            }
        }
        else
        {
            settingsFalseTime = Time.realtimeSinceStartup;
            if (Time.realtimeSinceStartup < settingsCloseTime + 0.2f)
            {
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}
