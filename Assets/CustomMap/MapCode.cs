using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MapCode : MonoBehaviour
{
    public MapSelectButton CloneButton;
    public Text text;
    public string path;
    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath;
        path += "/Workshop";
        Directory.CreateDirectory(path);

        float yPos = 128;
        PlayerPrefs.SetInt("Custom", 1);
        PlayerPrefs.Save();
        string[] files = System.IO.Directory.GetFiles(path, "*.ffwt");
        foreach (string s in files)
        {
            GameObject c = Instantiate(CloneButton.gameObject);
            c.SetActive(true);
            c.transform.parent = CloneButton.transform.parent;
            yPos -= 150;
            c.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            Debug.Log(path + " PATH LENGTH IS " + path.Length + " FULL LENGTH IS " + s.Length);
            string finalVer = s;
            c.gameObject.GetComponent<MapSelectButton>().ItemName = finalVer;
            c.gameObject.GetComponent<MapSelectButton>().finalName = Path.GetFileName(s);
            c.GetComponent<RectTransform>().position = new Vector3(0, yPos, 0);

        }
    }
    void Update()
    {
        PlayerPrefs.SetString("MapEditor", text.text);
        PlayerPrefs.Save();
    }
    public void newLevel()
    {
        string selectedPath = path + "/Map_" + Time.time.ToString() + ".ffwt";
        System.IO.File.WriteAllText(selectedPath, "");
        PlayerPrefs.SetString("ChosenDir", selectedPath);
        PlayerPrefs.SetInt("Custom", 0);
        PlayerPrefs.SetString("MapEditor", "");
        PlayerPrefs.Save();
        SceneManager.LoadScene("MapEditor");
    }
    public void loadScene()
    {
        SceneManager.LoadScene("MapEditor");
    }
    public void playLevel()
    {
        SceneManager.LoadScene("MapEditor");

    }
}
