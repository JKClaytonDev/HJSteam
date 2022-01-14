using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder;
public class SetAllShaderValues : MonoBehaviour
{
    public Shader basicShader;
    // Start is called before the first frame update
    void Start()
    {
        SetPS1Jagged(PlayerPrefs.GetInt("PS1") == 1);
    }
    public void togglePS1()
    {
        int PS1 = 0;
        if (PlayerPrefs.GetInt("PS1") == 0)
            PS1 = 1;
        PlayerPrefs.SetInt("PS1", PS1);
        PlayerPrefs.Save();
        SetPS1Jagged(PlayerPrefs.GetInt("PS1") == 1);
    }
    public void SetPS1Jagged(bool b)
    {
        foreach (ProBuilderMesh pbm in FindObjectsOfType<ProBuilderMesh>())
        {
            foreach (Material m in pbm.GetComponent<Renderer>().materials)
            {
                if (m.shader == basicShader)
                {
                    if (b)
                    {
                        m.SetInt("VertexSimplification", 1);
                        m.SetFloat("VSRatio", 25);
                        m.SetFloat("VSBase", 3);
                        Debug.Log("CHANGED SHADER");
                    }
                    else
                    {
                        m.SetInt("VertexSimplification", 0);
                        m.SetFloat("VSRatio", 0);
                        m.SetFloat("VSBase", 0);
                        Debug.Log("CHANGED SHADER");
                    }
                }
            }
        }
    }

}
