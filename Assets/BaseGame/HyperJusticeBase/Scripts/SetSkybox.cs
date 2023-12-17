using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSkybox : MonoBehaviour
{
    public Material[] materials;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = materials[Random.Range(0, materials.Length - 1)];
    }
    private void Update()
    {
        Time.timeScale = 1;
    }

}
