using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTrigger : MonoBehaviour
{
    public string sceneName;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<RobotController>())
        SceneManager.LoadScene(sceneName);
    }
}
