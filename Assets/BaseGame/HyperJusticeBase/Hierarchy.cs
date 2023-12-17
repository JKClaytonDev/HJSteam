using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Hierarchy : MonoBehaviour
{
    public GameObject button;
    float refreshTime = 0;
    public MapMakerCamera cam;
    public GameObject SelectedRenderer;
    public Text selectedInfo;
    public Text selectedPos;
    public Text selectedRot;
    public Text selectedScale;
    // Update is called once per frame
    void Update()
    {
        SelectedRenderer.transform.parent = null;
        if (!cam.selectedObject)
        {
            SelectedRenderer.SetActive(false);
            selectedInfo.gameObject.SetActive(false);
        }
        else
        {
            SelectedRenderer.SetActive(true);
            selectedInfo.gameObject.SetActive(true);
            selectedInfo.text = cam.selectedObject.name;
            selectedPos.text = cam.selectedObject.transform.position.ToString();
            selectedRot.text = cam.selectedObject.transform.eulerAngles.ToString();
            selectedScale.text = cam.selectedObject.transform.localScale.ToString();
            GameObject c2 = cam.selectedObject;
            SelectedRenderer.GetComponent<Renderer>().GetComponent<MeshFilter>().mesh = c2.GetComponent<MeshFilter>().mesh;
            SelectedRenderer.GetComponent<Renderer>().transform.position = c2.transform.position;
            SelectedRenderer.GetComponent<Renderer>().transform.rotation = c2.transform.rotation;
            SelectedRenderer.GetComponent<Renderer>().transform.localScale = c2.transform.localScale * 1.1f;
        }
        if (Time.realtimeSinceStartup > refreshTime)
        {
            int pos = 120;
            Vector3 v3Pos = button.transform.position;
            foreach (HierarchyButton h in FindObjectsOfType<HierarchyButton>())
            {
                Destroy(h.gameObject);
            }
            foreach (CubeMover c in FindObjectsOfType<CubeMover>())
            {
                GameObject k = Instantiate(button);
                k.SetActive(true);
                k.transform.parent = button.transform.parent;
                v3Pos -= new Vector3(0, 150, 0);
                k.transform.position = v3Pos;
                k.GetComponent<HierarchyButton>().text = c.gameObject.name;
                k.transform.localScale = new Vector3(1, 1, 1);
                k.SetActive(true);
            }
            refreshTime += 1;
        }
    }
}
