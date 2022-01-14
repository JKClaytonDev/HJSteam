using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
using UnityEngine.UI;
public class WorkshopUpload : MonoBehaviour
{
    public SteamWorkshop s;
    public Text titleText;
    public Text titleDesc;
    public Text titlePath;
    public Text imagePath;
    private void Start()
    {
        AppId_t t = new AppId_t();
        t.m_AppId = 1831780;
        SteamAPI.RestartAppIfNecessary(t);
        SteamAPI.Init();
    }
    public void FinalWorkshopUpload()
    {

        s.UploadContent(titleText.text, titleDesc.text, titlePath.text, new string[] { titleDesc.text, titleDesc.text }, imagePath.text);
    }
}