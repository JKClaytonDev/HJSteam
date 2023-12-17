using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
public class MapSelectButton : MonoBehaviour
{
    public Text t;
    public string ItemName;
    public Text levelName;
    public string finalName;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

        t.text = finalName;
    }
    public void loadMap()
    {
        PlayerPrefs.DeleteKey("MapEditor");
        PlayerPrefs.SetString("ChosenDir", ItemName);
        PlayerPrefs.SetInt("Custom", 1);
        PlayerPrefs.SetString("MapEditor", ItemName);
        FindObjectOfType<MapData>().mapData = ItemName;
        PlayerPrefs.Save();
        Debug.Log("PLAYERPREFS " + PlayerPrefs.GetString("MapEditor"));
        PlayerPrefs.Save();
        levelName.text = finalName;
        //SceneManager.LoadScene("  ");
    }
    public void Select()
    {
        GameObject.Find("LevelName").GetComponent<Text>().text = ItemName;
        FindObjectOfType<MapData>().fullString = ItemName;
    }
}
