using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class storyText : MonoBehaviour
{
    public string[] text;
    public Sprite[] images;
    public Renderer r;
    int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("VOL");
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        Texture t = GetComponent<defaultBG>().background.texture;
        if (images[index] != null)
            t = images[index].texture;
        r.material.SetTexture("_Texture2D", t);
        Debug.Log("CHANGED TEXTURE" + r.material.GetTexture("_Texture2D"));
        GetComponent<Text>().text = text[index];
        if (Input.GetKeyDown(KeyCode.Space))
            index++;
        if (index >= text.Length)
        {
            Debug.Log("LOADING SCENE " + SceneManager.GetSceneByBuildIndex(SceneManager.GetActiveScene().buildIndex + 1).name);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
